using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class SmallEnemyV4 : MonoBehaviour
{
    public float maxHealth = 50f;
    public float currentHealth;
    public int attackDamage = 10;
    public float attackRange = 2f;
    public float maxChaseRange = 10f;
    public float patrolSpeed = 3f;
    public float chaseSpeed = 5f;
    public float patrolRadius = 10f; // Radius for random patrolling
    public Transform originalPosition;
    public float rotationSpeed = 5f;
    public float attackCooldown = 2f;
    private float lastAttackTime;
    private Animator animator;
    private Transform player;
    private NavMeshAgent navMeshAgent;
    public float moveSpeed = 5f;
    public float moveDistance = 9f; // Set the distance to move
    private bool isBeingDrivenAway = false;
    private bool hasReachedPlayer = false;
    private Vector3 randomPatrolPoint;

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        navMeshAgent = GetComponent<NavMeshAgent>();

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

        SetRandomPatrolPoint();
    }

    void Update()
    {
        if (isBeingDrivenAway)
        {
            // Check if the enemy has reached the destination
            if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.1f)
            {
                // Reset the flag and perform any necessary actions
                isBeingDrivenAway = false;
            }

            // Optionally, you can return early or add additional logic specific to being driven away
            return;
        }

        // Rest of your Update() method for normal behaviors (attack, chase, patrol)
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        PatrolRandomly();
        /*if (distanceToPlayer <= attackRange)
        {
           *//* Attack();*//*
            PatrolRandomly();

        }*/
        /*else if (distanceToPlayer <= maxChaseRange)
        {
            ChasePlayer();
        *//*}*//*
        else
        {
            PatrolRandomly();
        }*/
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the entering collider is the one you want to react to
        if (other.CompareTag("Obor"))
        {
            // Move the game object away from the collider using NavMeshAgent
            Vector3 moveDirection = transform.position - other.transform.position;
            Vector3 newPosition = transform.position + moveDirection.normalized * moveDistance;

            // Set the destination for smooth movement
            navMeshAgent.SetDestination(newPosition);

            // Set the flag to indicate that the enemy is being driven away
            isBeingDrivenAway = true;

            // Optional: You can also set a bool or trigger for running animation here if needed
            if (animator != null)
            {
                SetAnimatorBoolsFalse();
                animator.SetTrigger("IsRunning");
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Set the flag to indicate that the enemy has reached the player
            hasReachedPlayer = true;

            // Optional: You can also set a bool or trigger for idle animation here if needed
            if (animator != null)
            {
                SetAnimatorBoolsFalse();
                animator.SetTrigger("IsIdle");
            }
        }
    }

    void ChasePlayer()
    {
        if (!hasReachedPlayer)
        {
            navMeshAgent.speed = chaseSpeed;
            navMeshAgent.SetDestination(player.position);

            if (animator != null)
            {
                SetAnimatorBoolsFalse();
                animator.SetTrigger("IsRunning");
            }
        }
        else
        {
            // Enemy has reached the player, so set to idle or any other desired behavior
            if (animator != null)
            {
                SetAnimatorBoolsFalse();
                animator.SetTrigger("IsIdle");
            }
        }
    }

    void PatrolRandomly()
    {
        // Check if the enemy has reached the random patrol point
        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.1f)
        {
            SetRandomPatrolPoint();
        }

        if (animator != null)
        {
            SetAnimatorBoolsFalse();
            animator.SetTrigger("IsRunning");
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

    void SetRandomPatrolPoint()
    {
        // Generate a random point within the designated patrol area
        Vector3 randomOffset = Random.insideUnitSphere * patrolRadius;
        randomOffset.y = 0f; // Keep the movement in the horizontal plane
        randomPatrolPoint = originalPosition.position + randomOffset;

        // Set the destination for random patrolling
        navMeshAgent.SetDestination(randomPatrolPoint);
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
}
