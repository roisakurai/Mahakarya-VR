using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    public int damage = 10;

    // Method untuk menangani tabrakan dengan musuh
    private void OnTriggerEnter(Collider other)
    {
        // Cek apakah objek yang tertabrak memiliki tag "Enemy" (musuh)
        if (other.CompareTag("Enemy"))
        {
            // Dapatkan komponen EnemyHealth dari objek musuh
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();

            // Jika objek musuh memiliki komponen EnemyHealth
            if (enemyHealth != null)
            {
                // Berikan kerusakan kepada musuh
                enemyHealth.TakeDamage(damage);
            }
        }
    }
}
