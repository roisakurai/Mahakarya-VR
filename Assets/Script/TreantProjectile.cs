using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreantProjectile : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 10;

    void Start()
    {
        // Mendapatkan komponen Rigidbody (opsional jika ingin mengatur kecepatan)
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = transform.forward * speed;
        }
        else
        {
            Debug.LogError("Rigidbody component not found on bullet.");
        }

        // Destroy the bullet after a certain time to avoid cluttering the scene
        Destroy(gameObject, 2f);
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger detected with: " + other.gameObject.name); // Debug log

        if (other.CompareTag("Player"))
        {
            Debug.Log("Bullet hit the player."); // Debug log

            // Damage the player
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                // Destroy the bullet immediately
                Destroy(gameObject);
                playerHealth.TakeDamage(damage);
            }

        }
    }
}
