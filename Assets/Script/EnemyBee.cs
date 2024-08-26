using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBee : MonoBehaviour
{
    public float maxHealth = 50f;
    public float currentHealth;
    public float attackDamage = 10f;
    public float attackRange = 2f;
    public float patrolSpeed = 3f;
    public float patrolRadius = 10f; // Radius for random patrolling
    public Transform originalPosition;
    public float rotationSpeed = 5f;
    public float attackCooldown = 2f;
    public float projectileSpeed = 10f; // Speed of the projectile
    public float projectileLifetime = 5f; // Lifetime of the projectile
    public Transform attackPoint; // Point where projectiles are spawned
    public GameObject projectilePrefab; // Prefab of the projectile
    private float lastAttackTime;
    private Animator animator;
    private Transform player;
    private NavMeshAgent navMeshAgent;
    private bool isBeingDrivenAway = false;
    private Vector3 randomPatrolPoint;
    public float moveDistance = 5f; // Adjust the value according to your needs
    public float projectileAttackRange = 10f; // Maximum distance for projectile attacks

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        navMeshAgent = GetComponent<NavMeshAgent>();

        if (navMeshAgent == null || !navMeshAgent.isActiveAndEnabled)
        {
            Debug.LogError("NavMeshAgent component is missing or not enabled. Please check the setup.");
        }

        navMeshAgent.speed = patrolSpeed;

        SetRandomPatrolPoint();
    }

    void Update()
    {
        if (isBeingDrivenAway)
        {
            if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.1f)
            {
                isBeingDrivenAway = false;
            }
            return;
        }

        // Check if the player is within the attack range
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= projectileAttackRange)
        {
            // Player is within the attack range
            Attack();
        }
        else
        {
            // Player is outside attack range
            PatrolRandomly();
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
            animator.SetTrigger("IsRunning"); // Set the running animation trigger
        }
    }

    void SetRandomPatrolPoint()
    {
        // Generate a random point within the designated patrol area
        Vector3 randomOffset = Random.insideUnitSphere * patrolRadius;
        randomOffset.y = 0f; // Keep the movement in the horizontal plane
        randomPatrolPoint = originalPosition.position + randomOffset;

        // Set the destination for random patrolling
        navMeshAgent.SetDestination(randomPatrolPoint);

        if (animator != null)
        {
            SetAnimatorBoolsFalse();
            animator.SetTrigger("IsRunning"); // Set the running animation trigger
        }
    }

    void Attack()
    {
        if (Time.time - lastAttackTime >= attackCooldown)
        {
            // Look directly at the player
            transform.LookAt(player.position);

            if (animator != null)
            {
                SetAnimatorBoolsFalse();
                animator.SetTrigger("IsAttacking");

                // Invoke the method to shoot a projectile after a delay
                Invoke("SpawnProjectile", 1.5f);

                lastAttackTime = Time.time;
            }
        }
    }

    void SpawnProjectile()
    {
        // Instantiate a projectile at the attack point position and rotation
        GameObject projectile = Instantiate(projectilePrefab, attackPoint.position, attackPoint.rotation);

        // Get the Rigidbody component of the projectile
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();

        // Calculate the direction towards the player
        Vector3 directionToPlayer = (player.position - attackPoint.position).normalized;

        // Apply force to the projectile in the direction towards the player
        projectileRb.AddForce(directionToPlayer * projectileSpeed, ForceMode.Impulse);

        // Destroy the projectile after a certain time (adjust as needed)
        Destroy(projectile, projectileLifetime);
    }

    void SetAnimatorBoolsFalse()
    {
        animator.SetBool("IsRunning", false);
        animator.SetBool("IsAttacking", false);
        animator.SetBool("IsTakingDamage", false);
        animator.SetBool("IsIdle", false);
    }
}
