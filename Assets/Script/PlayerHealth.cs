using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public Image healthBarFill;

    // Event yang dipanggil saat pemain mati
    public UnityEvent onPlayerDeath;

    public void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // Ensure health doesn't drop below zero
        currentHealth = Mathf.Max(currentHealth, 0);

        UpdateHealthBar();

        // Check if health drops to zero
        if (currentHealth == 0)
        {
            Die();
        }

        // Debug message to show the current health after taking damage
        Debug.Log("Player health: " + currentHealth);
    }

    private void UpdateHealthBar()
    {
        // Update health bar fill amount based on current health and max health
        float fillAmount = (float)currentHealth / maxHealth;
        healthBarFill.fillAmount = fillAmount;
    }

    public void Die()
    {
        // Perform death actions here, such as disabling player control, playing death animation, etc.
        Debug.Log("Player has died.");
        // Full the HP Bar
        currentHealth = maxHealth;
        UpdateHealthBar();

        // Panggil event onPlayerDeath
        if (onPlayerDeath != null)
        {
            onPlayerDeath.Invoke();
        }
    }
}
