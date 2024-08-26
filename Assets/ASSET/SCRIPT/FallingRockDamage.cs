using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingRockDamage : MonoBehaviour
{
    public UnityEngine.Events.UnityEvent destroyStone; // Event to trigger on collision
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                destroyStone.Invoke();
                playerHealth.TakeDamage(10); // Mengurangi health player sebanyak 10
            }
        }
    }
}
