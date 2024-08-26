using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SmallEnemy : MonoBehaviour
{
    public float maxHealth = 20f;
    public float currentHealth;
    public int attackDamage = 2;
    public float attackRange = 2f;
    public float maxChaseRange = 10f;
    public float patrolSpeed = 1f;
    public float chaseSpeed = 3f;
    public float rotationSpeed = 2f;
    public float attackCooldown = 2f;
    private float lastAttackTime;
    private Animator animator;
    private Transform player;
    private UnityEngine.AI.NavMeshAgent navMeshAgent;

    public Transform[] patrolWaypoints;
    private int currentWaypointIndex = 0;

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();

        // Check if navMeshAgent is null or not active, then enable it
        if (navMeshAgent == null)
        {
            Debug.LogError("NavMeshAgent component is missing. Please attach it to the GameObject.");
        }
        else if (!navMeshAgent.isActiveAndEnabled)
        {
            navMeshAgent.enabled = true;
        }

        navMeshAgent.speed = patrolSpeed;

        SetNextWaypoint();
    }

    void Update()
    {
        // Rest of your Update() method for normal behaviors (attack, chase, patrol)
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
            SetAnimatorBoolsFalse();
            animator.SetTrigger("IsRunning"); //aslinya bool and condition is true
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
                SetNextWaypoint();
            }
        }

        if (animator != null)
        {
            SetAnimatorBoolsFalse();
            animator.SetTrigger("IsRunning"); //aslinya bool and condition is true
        }
    }


    void SetNextWaypoint()
    {
        // Set the destination to the next waypoint
        navMeshAgent.SetDestination(patrolWaypoints[currentWaypointIndex].position);

        // Increment the waypoint index, and loop back to the start if necessary
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
            }

            if (animator != null)
            {
                SetAnimatorBoolsFalse();
                animator.SetTrigger("IsAttacking");
            }
        }
    }

    void Die()
    {
        if (animator != null)
        {
            SetAnimatorBoolsFalse();
            animator.SetTrigger("Die");
        }

        // Optional: Add logic for any actions you want to perform when the enemy dies
        Destroy(gameObject);
    }

    void SetAnimatorBoolsFalse()
    {
        animator.SetBool("IsRunning", false);
        animator.SetBool("IsAttacking", false);
        animator.SetBool("IsTakingDamage", false);
        animator.SetBool("IsIdle", false);
    }
}
