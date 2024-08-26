using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 50;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // Check if health drops to or below zero
        if (currentHealth <= 0)
        {
            Die();
        }

        // Debug message to show the current health after taking damage
        Debug.Log("Enemy health: " + currentHealth);
    }

    void Die()
    {
        // Perform death actions here, such as playing death animation, giving player points, etc.
        Debug.Log("Enemy has died.");
        // For example, you can deactivate the GameObject to visually represent the death
        Destroy(gameObject);
    }

}