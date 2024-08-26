using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeeHealth : MonoBehaviour
{
    public static BeeHealth instance;

    public int maxHealth = 4;
    public int currentHealth;

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
        animator.SetBool("isDie", true);
        // Menghapus boss dari scene setelah 1 detik
        Invoke("DestroyBoss", 3f);
    }

    void DestroyBoss()
    {
        Destroy(gameObject);
    }

}
