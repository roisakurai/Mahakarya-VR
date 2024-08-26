using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyChest : MonoBehaviour
{
    public float maxHealth = 25f;
    public float currentHealth;
    public int attackDamage = 5;
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
    private int damageCount = 0;

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

        if (distanceToPlayer <= attackRange)
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
        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.1f)
        {
            navMeshAgent.isStopped = true;

            Vector3 directionToNextWaypoint = (patrolWaypoints[currentWaypointIndex].position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(directionToNextWaypoint);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);

            float angleToNextWaypoint = Quaternion.Angle(transform.rotation, lookRotation);
            if (angleToNextWaypoint < 5f)
            {
                navMeshAgent.isStopped = false;
                SetNextWaypointDestination();
            }
        }

        if (animator != null)
        {
            animator.SetTrigger("isPatrol");
        }
    }

    void SetNextWaypointDestination()
    {
        navMeshAgent.SetDestination(patrolWaypoints[currentWaypointIndex].position);
        currentWaypointIndex = (currentWaypointIndex + 1) % patrolWaypoints.Length;
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

    private void ActivateDeathAnimation()
    {
        if (animator != null)
        {
            animator.SetTrigger("isDie");
        }

        navMeshAgent.isStopped = true;
        StartCoroutine(DestroyAfterDeathAnimation());
    }

    private void DamageAnimation()
    {
        // Mengaktifkan animasi kematian ("Die") atau melakukan tindakan lainnya
        if (animator != null)
        {
            animator.SetTrigger("isDie");
        }

        // Hentikan pergerakan atau tindakan lainnya sesuai kebutuhan
        navMeshAgent.isStopped = true;
    }


    private IEnumerator DestroyAfterDeathAnimation()
    {
        yield return new WaitForSeconds(3.0f);
        Destroy(gameObject);
    }
}
