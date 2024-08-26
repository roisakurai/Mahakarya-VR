using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBat : MonoBehaviour
{
    public float maxHealth = 50f;
    public float currentHealth;
    public int attackDamage = 2;
    public float attackRange = 2f;
    public float maxChaseRange = 10f;
    public float patrolSpeed = 3f;
    public float chaseSpeed = 5f;
    public float rotationSpeed = 5f;
    public float attackCooldown = 2f;
    public Transform[] patrolWaypoints; // Array for waypoint transforms
    private int currentWaypointIndex = 0;
    private float lastAttackTime;
    private Animator animator;
    private Transform player;
    private NavMeshAgent navMeshAgent;

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        navMeshAgent = GetComponent<NavMeshAgent>();

        if (navMeshAgent == null)
        {
            Debug.LogError("NavMeshAgent component is missing. Please attach it to the GameObject.");
        }
        else if (!navMeshAgent.isActiveAndEnabled)
        {
            navMeshAgent.enabled = true;
        }

        navMeshAgent.speed = patrolSpeed;
        SetNextWaypointDestination();
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        bool isCloseToTorch = false;

        GameObject[] torches = GameObject.FindGameObjectsWithTag("Obor");
        foreach (GameObject torch in torches)
        {
            float distanceToTorch = Vector3.Distance(transform.position, torch.transform.position);
            if (distanceToTorch < 5f) // Adjust the distance as needed
            {
                isCloseToTorch = true;
                break;
            }
        }

        if (isCloseToTorch)
        {
            FleeFromTorch();
        }
        else if (distanceToPlayer <= attackRange)
        {
            Attack();
        }
        else if (distanceToPlayer <= maxChaseRange)
        {
            ChasePlayer();
        }
        else
        {
            Patrol();
        }
    }

    void ChasePlayer()
    {
        navMeshAgent.speed = chaseSpeed;
        navMeshAgent.SetDestination(player.position);

        if (animator != null)
        {
            animator.SetTrigger("isChase");
        }
    }

    void Patrol()
    {
        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance <= 0.1f)
        {
            SetNextWaypointDestination();
        }

        if (animator != null)
        {
            animator.SetTrigger("isPatrol");
        }
    }

    void FleeFromTorch()
    {
        GameObject nearestTorch = null;
        float minDistance = float.MaxValue;
        GameObject[] torches = GameObject.FindGameObjectsWithTag("Obor");
        foreach (GameObject torch in torches)
        {
            float distanceToTorch = Vector3.Distance(transform.position, torch.transform.position);
            if (distanceToTorch < minDistance)
            {
                minDistance = distanceToTorch;
                nearestTorch = torch;
            }
        }

        Vector3 fleeDirection = transform.position - nearestTorch.transform.position;
        Vector3 newDestination = transform.position + fleeDirection.normalized * 10f; // Flee distance
        navMeshAgent.SetDestination(newDestination);

        if (animator != null)
        {
            animator.SetTrigger("isPatrol");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Lamp"))
        {
            ActivateDeathAnimation();
        }
    }

    void SetNextWaypointDestination()
    {
        if (patrolWaypoints.Length == 0)
            return;

        navMeshAgent.SetDestination(patrolWaypoints[currentWaypointIndex].position);
        currentWaypointIndex = (currentWaypointIndex + 1) % patrolWaypoints.Length;
    }

    void Attack()
    {
        if (Time.time - lastAttackTime >= attackCooldown)
        {
            StartCoroutine(ContinuousAttack());
        }
    }

    IEnumerator ContinuousAttack()
    {
        while (Vector3.Distance(transform.position, player.position) <= attackRange)
        {
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(attackDamage);
                Debug.Log("Player Health: " + playerHealth.currentHealth);
            }

            if (animator != null)
            {
                animator.SetTrigger("isAttack");
            }

            lastAttackTime = Time.time;
            yield return new WaitForSeconds(attackCooldown);
        }
    }

    private void ActivateDeathAnimation()
    {
        if (animator != null)
        {
            animator.SetTrigger("isDie");
        }

        navMeshAgent.isStopped = true;
        StartCoroutine(DestroyAfterDeathAnimation());
    }

    private IEnumerator DestroyAfterDeathAnimation()
    {
        yield return new WaitForSeconds(3.0f);
        Destroy(gameObject);
    }
}
