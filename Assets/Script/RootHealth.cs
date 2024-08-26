
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class RootHealth : MonoBehaviour
{
    public int maxHealth = 10; // Maximum health of the root
    private int currentHealth; // Current health of the root
    public EnemyTreant enemyTreant;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    // Method to take damage
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // Check if the root is destroyed
        if (currentHealth <= 0)
        {
            DestroyRoot();
        }
    }

    // Method to handle root destruction
    private void DestroyRoot()
    {
        // Perform any additional logic for root destruction
        // For example, play an animation, spawn particles, etc.

        // Destroy the root GameObject
        if (enemyTreant != null)
        {
            enemyTreant.RootDestroyed();
        }

        // Destroy the root GameObject
        Destroy(gameObject);
    }
}

