using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 10;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    // Metode untuk menerima kerusakan
    public void TakeDamage(int damage)
    {
        // Kurangi kesehatan dengan jumlah kerusakan
        currentHealth -= damage;

        // Jika kesehatan mencapai atau di bawah nol, panggil metode Die()
        if (currentHealth <= 0)
        {
            Die();
        }
    }


    // Metode yang dipanggil ketika objek mati
    void Die()
    {
        // Hancurkan objek dari dunia permainan
        Destroy(gameObject);
    }
}

