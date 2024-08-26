using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SmallEnemyMushroom : MonoBehaviour
{
    public float maxHealth = 50f;
    public float currentHealth;
    public int attackDamage = 2;
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
            PatrolRandomly();
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

    void PatrolRandomly()
    {
        // Check if the enemy has reached the random patrol point
        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.1f)
        {
            SetRandomPatrolPoint();
        }

        // Set the destination for random patrolling
        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.5f)
        {
            SetRandomPatrolPoint();
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
            // Aktifkan animasi kematian ("Die") atau jalankan fungsi lain yang sesuai
            ActivateDeathAnimation();
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
        // Mengaktifkan animasi kematian ("Die") atau melakukan tindakan lainnya
        animator.SetBool("IsRunning", false);
        animator.SetBool("IsAttacking", false);
        animator.SetBool("IsDie", false);
        animator.SetBool("IsTakingDamage", true);

        // Hentikan pergerakan atau tindakan lainnya sesuai kebutuhan
        navMeshAgent.isStopped = true;

        // Jalankan fungsi lain yang diperlukan, misalnya menghancurkan objek setelah animasi selesai
        /*StartCoroutine(DestroyAfterDeathAnimation());*/
    }

    private void ActivateDeathAnimation()
    {
        // Mengaktifkan animasi kematian ("Die") atau melakukan tindakan lainnya
        animator.SetBool("isRunning", false);
        animator.SetBool("isAttacking", false);
        animator.SetBool("IsDie", true);

        // Hentikan pergerakan atau tindakan lainnya sesuai kebutuhan
        navMeshAgent.isStopped = true;

        // Jalankan fungsi lain yang diperlukan, misalnya menghancurkan objek setelah animasi selesai
        StartCoroutine(DestroyAfterDeathAnimation());
    }

    private IEnumerator DestroyAfterDeathAnimation()
    {
        // Tunggu sampai durasi animasi kematian selesai (sesuaikan dengan durasi animasi Anda)
        yield return new WaitForSeconds(3.0f);

        // Hancurkan objek
        Destroy(gameObject);
        /*Score.instance.AddScore(addPoints);*/

    }

}
