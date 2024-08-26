using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaController : MonoBehaviour
{
    public int damagePerSecond = 10;  // Jumlah damage per detik
    private Dictionary<GameObject, Coroutine> playersInLava = new Dictionary<GameObject, Coroutine>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!playersInLava.ContainsKey(other.gameObject))
            {
                Coroutine damageCoroutine = StartCoroutine(DamagePlayer(other.gameObject));
                playersInLava.Add(other.gameObject, damageCoroutine);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (playersInLava.ContainsKey(other.gameObject))
            {
                StopCoroutine(playersInLava[other.gameObject]);
                playersInLava.Remove(other.gameObject);
            }
        }
    }

    private IEnumerator DamagePlayer(GameObject PlayerController)
    {
        PlayerHealth playerHealth = PlayerController.GetComponent<PlayerHealth>();

        while (true)
        {
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damagePerSecond);
            }
            yield return new WaitForSeconds(1.0f);
        }
    }
}