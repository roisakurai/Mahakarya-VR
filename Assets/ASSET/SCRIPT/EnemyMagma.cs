using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMagma : MonoBehaviour
{
    public float maxHealth = 50f;
    public float currentHealth;
    public int attackDamage = 2;
    public float attackRange = 2f;
    public float maxChaseRange = 10f;
    public float patrolSpeed = 3f;
    public float chaseSpeed = 5f;
    public Transform[] patrolPoints;
    public float rotationSpeed = 5f;
    public float attackCooldown = 2f;
    private float lastAttackTime;
    private Animator animator;
    private Transform player;
    private NavMeshAgent navMeshAgent;

    private int currentPatrolIndex = 0;

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

        if (patrolPoints.Length > 0)
        {
            PatrolManually();
        }
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
            PatrolManually();
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

    void PatrolManually()
    {
        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.1f)
        {
            SetNextPatrolPoint();
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

    void SetNextPatrolPoint()
    {
        navMeshAgent.SetDestination(patrolPoints[currentPatrolIndex].position);
        currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
    }


    void Attack()
    {
        if (Time.time - lastAttackTime >= attackCooldown)
        {
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(attackDamage);
                lastAttackTime = Time.time;
                Debug.Log("Player Health: " + playerHealth.currentHealth);
            }

            if (animator != null)
            {
                animator.SetTrigger("isAttack");
            }
        }
    }

    private void DamageAnimation()
    {
        if (animator != null)
        {
            animator.SetTrigger("isDie");
        }

        navMeshAgent.isStopped = true;
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
