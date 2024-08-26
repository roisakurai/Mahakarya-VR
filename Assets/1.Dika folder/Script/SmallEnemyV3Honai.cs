using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SmallEnemyV3Honai : MonoBehaviour
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
            animator.SetBool("IsRunning", true);
            animator.SetBool("IsAttacking", false);
            animator.SetBool("IsTakingDamage", false);
            animator.SetBool("IsIdle", false);
        }
    }

    void Patrol()
    {
        // Check if the enemy has reached the patrol waypoint
        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.1f)
        {
            // Stop the enemy's movement
            navMeshAgent.isStopped = true;

            // Rotate towards the next waypoint
            Vector3 directionToNextWaypoint = (patrolWaypoints[currentWaypointIndex].position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(directionToNextWaypoint);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);

            // Check if the enemy has finished rotating
            float angleToNextWaypoint = Quaternion.Angle(transform.rotation, lookRotation);
            if (angleToNextWaypoint < 5f) // You can adjust this threshold as needed
            {
                // Resume the enemy's movement towards the next waypoint
                navMeshAgent.isStopped = false;
                
                // Set the destination to the next waypoint
                SetNextWaypointDestination();
            }
        }

        if (animator != null)
        {
            animator.SetBool("IsRunning", true);
            animator.SetBool("IsAttacking", false);
            animator.SetBool("IsTakingDamage", false);
            animator.SetBool("IsIdle", false);
        }
    }

    void OnTriggerEnter(Collider trigger)
    {
        if (trigger.CompareTag("Gas"))
        {
            DamageAnimation();
            ActivateDeathAnimation();
        }
    }

    void SetNextWaypointDestination()
    {
        // Set the destination to the next waypoint in the array
        navMeshAgent.SetDestination(patrolWaypoints[currentWaypointIndex].position);

        // Increment the waypoint index or loop back to the start if reached the end
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
                animator.SetBool("IsRunning", false);
                animator.SetBool("IsAttacking", true);
                animator.SetBool("IsTakingDamage", false);
                animator.SetBool("IsIdle", false);
            }
        }
    }

    private void DamageAnimation()
    {
        animator.SetBool("IsRunning", false);
        animator.SetBool("IsAttacking", false);
        animator.SetBool("IsDie", false);
        animator.SetBool("IsTakingDamage", true);

        navMeshAgent.isStopped = true;
    }

    private void ActivateDeathAnimation()
    {
        animator.SetBool("isRunning", false);
        animator.SetBool("isAttacking", false);
        animator.SetBool("IsDie", true);

        navMeshAgent.isStopped = true;

        StartCoroutine(DestroyAfterDeathAnimation());
    }

    private IEnumerator DestroyAfterDeathAnimation()
    {
        yield return new WaitForSeconds(3.0f);

        Destroy(gameObject);
    }
}
