using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBreathDamage : MonoBehaviour
{
    public int damagePerSecond = 10;  // Jumlah damage per detik

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damagePerSecond);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Tidak perlu melakukan apapun saat pemain keluar dari area
    }
}
