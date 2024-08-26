using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GolemHealth : MonoBehaviour
{
    public static GolemHealth instance;

    public int maxHealth = 5;
    public int currentHealth;

    private Animator animator;

    // Tambahkan referensi ke health bar UI
    public Slider healthBarSlider;

    // Event Untuk Golem Mati
    public UnityEvent DeathGolem;

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

    public void TakeDamage(int damageAmount)
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
        if (DeathGolem != null)
        {
            DeathGolem.Invoke();
        }
    }
}
