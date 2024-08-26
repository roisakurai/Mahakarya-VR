using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class EnemyTreant : MonoBehaviour
{
    public float attackPhaseDuration = 5f;
    public float spawnRootsPhaseDuration = 5f;
    public float retractRootsPhaseDuration = 1f;
    public GameObject projectilePrefab;
    public Transform attackPoint;
    private int destroyedRootCount = 0;
    public int requiredDestroyedRoots = 4;
    public float projectileForce = 10f;
    public float attackCooldown = 2f;
    public float projectileLifetime = 2f;
    public float attackRange = 5f;
    public AudioClip projectileHitSound;// List to store the root GameObjects
    private List<Animator> rootAnimators;
    public List<GameObject> roots; // List to store the root GameObjects
    public AudioClip bossDestroyedSound;
    private bool spawnState = false;
    private bool retractState = false;
    private bool idleState = false;


    private AudioSource audioSource;
    private GameObject player;
    private Animator bossAnimator;
    private float nextAttackTime = 0f;

    private enum BossState
    {
        AttackPlayer,
        SpawnRoots,
        RetractRoots,
        Idle
    }

    private BossState currentState = BossState.AttackPlayer;
    private float stateTimer = 0f;
    private static readonly float AttackPhaseDuration = 5f;
    private static readonly float SpawnRetractPhaseDuration = 5f;

    private static readonly string SPAWN_TRIGGER = "Spawn";
    private static readonly string IDLE_TRIGGER = "Idle";
    private static readonly string DIE_TRIGGER = "Die"; // Name of the die trigger parameter

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("Player not found! Make sure the player has the tag 'Player'.");
        }

        bossAnimator = GetComponent<Animator>();
        destroyedRootCount = 0;
        // Get or add the AudioSource component
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Initialize state and timer
        currentState = BossState.AttackPlayer;
        stateTimer = AttackPhaseDuration;

        // Initialize rootAnimators list
        rootAnimators = new List<Animator>();

        foreach (GameObject root in roots)
        {
            if (root != null)
            {
                // Find the Animator component on each root GameObject
                Animator rootAnimator = root.GetComponent<Animator>();

                if (rootAnimator != null)
                {
                    // Add the Animator component to the list
                    rootAnimators.Add(rootAnimator);
                }
                else
                {
                    Debug.LogWarning($"Animator component not found on root GameObject: {root.name}");
                }
            }
        }
    }
    public void RootDestroyed()
    {
        destroyedRootCount++;

        // Check if the required number of roots are destroyed
        if (destroyedRootCount >= requiredDestroyedRoots)
        {
            DestroyBoss();
        }
    }
    void SpawnRoots()
    {
        // Implement logic to spawn roots
        // For example, activate root GameObjects in the roots list and trigger animations
        for (int i = 0; i < roots.Count; i++)
        {
            GameObject root = roots[i];
            Animator rootAnimator = rootAnimators[i];

            if (root != null)
            {
                // Activate the root GameObject
                root.SetActive(true);

                // Trigger animation if the root has an Animator component
                if (rootAnimator != null)
                {
                    // Assuming "Spawn" is the trigger for the spawn animation in the root Animator
                    rootAnimator.SetTrigger("Spawn");
                }
            }
        }
    }

    void RetractRoots()
    {
        // Implement logic to retract roots
        // For example, deactivate root GameObjects in the roots list and trigger animations
        for (int i = 0; i < roots.Count; i++)
        {
            GameObject root = roots[i];
            Animator rootAnimator = rootAnimators[i];

            if (root != null)
            {
                // Deactivate the root GameObject
                root.SetActive(false);

                // Trigger animation if the root has an Animator component
                if (rootAnimator != null)
                {
                    // Assuming "Retract" is the trigger for the retract animation in the root Animator
                    rootAnimator.SetTrigger("Retract");
                }
            }
        }
    }

    void Update()
    {
        // Update state based on timer
        stateTimer -= Time.deltaTime;

        Debug.Log($"Current State: {currentState}, Timer: {stateTimer}");

        switch (currentState)
        {
            case BossState.AttackPlayer:
                if (stateTimer <= 0f)
                {
                    // Attack phase completed, switch to spawn roots phase
                    currentState = BossState.SpawnRoots;
                    stateTimer = SpawnRetractPhaseDuration;
                    // Additional setup for spawn roots phase if needed
                    // Set the SPAWN_TRIGGER to start the Spawn animation
                    bossAnimator.SetTrigger(SPAWN_TRIGGER);
                }
                else
                {
                    // Continue attacking the player
                    Attack();
                }
                break;

            case BossState.SpawnRoots:
                // Additional setup for spawn roots phase if needed
                if (stateTimer <= 0f)
                {
                    // Spawn roots phase completed, switch to retract roots phase
                    currentState = BossState.RetractRoots;
                    stateTimer = SpawnRetractPhaseDuration;
                    // Set the IDLE_TRIGGER to start the Idle animation
                    bossAnimator.SetTrigger(IDLE_TRIGGER);
                }
                else
                {
                    // Spawn roots
                    SpawnRoots();
                }
                break;


            case BossState.RetractRoots:
                if (stateTimer <= 0f)
                {
                    // Retract roots phase completed, switch back to attack player phase
                    currentState = BossState.AttackPlayer;
                    stateTimer = attackPhaseDuration;
                    // Additional setup for attack player phase if needed
                }
                else
                {
                    // Retract roots
                    RetractRoots();
                }
                break;
        }

    }

    void Attack()
    {
        if (Time.time >= nextAttackTime)
        {
            // Smoothly rotate towards the player
            Quaternion targetRotation = Quaternion.LookRotation(player.transform.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 0.5f);

            // Start the attack coroutine
            StartCoroutine(AttackCoroutine());

            // Set the next attack time based on the cooldown
            nextAttackTime = Time.time + attackCooldown;
        }
    }

    IEnumerator AttackCoroutine()
    {
        // Delay before spawning the projectile
        yield return new WaitForSeconds(0.5f);

        // Set the shoot trigger to start the shooting animation
        bossAnimator.SetTrigger("Projectile Attack");

        // Instantiate a projectile at the attack point position and rotation
        GameObject projectile = Instantiate(projectilePrefab, attackPoint.position, attackPoint.rotation);

        // Get the Rigidbody component of the projectile
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();

        // Calculate the direction towards the player
        Vector3 directionToPlayer = (player.transform.position - attackPoint.position).normalized;

        // Apply force to the projectile in the direction towards the player
        projectileRb.AddForce(directionToPlayer * projectileForce, ForceMode.Impulse);

        // Set up the destruction of the projectile after a delay
        Destroy(projectile, projectileLifetime);

        // Play projectile hit sound
        PlayProjectileHitSound();
    }

    void PlayProjectileHitSound()
    {
        // Play sound if available
        if (projectileHitSound != null)
        {
            audioSource.PlayOneShot(projectileHitSound);
        }
    }

    // ... (remaining code)

    // Custom method to handle boss destruction
    public void DestroyBoss()
    {
        // Trigger the die animation
        bossAnimator.SetTrigger(DIE_TRIGGER);

        // For example, play a sound before destroying the boss
        PlayBossDestroyedSound();

        // Delay before destroying the boss GameObject (adjust as needed)
        StartCoroutine(DestroyBossCoroutine());
    }

    void PlayBossDestroyedSound()
    {
        // Play sound if available
        if (bossDestroyedSound != null)
        {
            audioSource.PlayOneShot(bossDestroyedSound);
        }
    }

    IEnumerator DestroyBossCoroutine()
    {
        // Delay before destroying the boss GameObject (adjust as needed)
        yield return new WaitForSeconds(2f);

        // Destroy the boss GameObject
        Destroy(gameObject);
    }
}
