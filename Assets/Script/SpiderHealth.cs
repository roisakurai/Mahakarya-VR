using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpiderHealth : MonoBehaviour
{
    public static SpiderHealth instance;

    public int maxHealth = 5;
    public int currentHealth;
    private UnityEngine.AI.NavMeshAgent navMeshAgent;

    private Animator animator;

    // Tambahkan referensi ke health bar UI
    public Slider healthBarSlider;

    
    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();

        // Pastikan health bar terinisialisasi dengan benar
        if (healthBarSlider != null)
        {
            healthBarSlider.maxValue = maxHealth;
            healthBarSlider.value = currentHealth;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamage (int damageAmount) 
    {
        currentHealth -= damageAmount;

        // Update nilai health bar
        if (healthBarSlider != null)
        {
            healthBarSlider.value = currentHealth;
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        navMeshAgent.isStopped = true;
        animator.SetBool("isDie", true);
        // Menghapus boss dari scene setelah 1 detik
        Invoke("DestroyBoss", 3f);
    }

    void DestroyBoss()
    {
        Destroy(gameObject);
    }

}
