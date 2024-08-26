using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyGolem : MonoBehaviour
{
    public static EnemyGolem instance;

    public int maxHealth = 10;
    private int currentHealth;

    private Animator animator;

    // Tambahkan referensi ke health bar UI
    public Slider healthBarSlider;

    
    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();

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

    public void TakeDamage (int damage) 
    {
        currentHealth -= damage;

        // Update nilai health bar
        if (healthBarSlider != null)
        {
            healthBarSlider.value = currentHealth;
        }

        if (currentHealth <= 0)
        {
            Die();
            animator.SetBool("isDie", true);
        }
    }

    void Die()
    {
        // // Logika kematian boss, misalnya:
        // Debug.Log("Boss Borobudur mati!");

        // // atau
        // Destroy(gameObject); // Menghapus boss dari scene secara permanen
    }
}
