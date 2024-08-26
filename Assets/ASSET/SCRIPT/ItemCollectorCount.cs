using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollectorCount : MonoBehaviour
{
    public static ItemCollectorCount instance;

    public int maxHealth = 3;
    public int currentHealth;

    private Animator animator;

    // Reference to the health bar UI
    public Slider healthBarSlider;

    // Define a public event for death
    public UnityEngine.Events.UnityEvent OnDeath;

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();

        // Ensure the health bar is properly initialized
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

        // Update health bar value
        if (healthBarSlider != null)
        {
            healthBarSlider.value = currentHealth;
        }

        if (currentHealth <= 0)
        {
            OnDeath.Invoke();
        }
    }
}
