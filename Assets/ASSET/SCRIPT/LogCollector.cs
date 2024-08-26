using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogCollector : MonoBehaviour
{
    public GameObject gameObjectToActivate; // Game object yang ingin diaktifkan setelah mengumpulkan 5 log
    public GameObject boundaryDeactive;
    public string logTag = "Log"; // Tag untuk log
    private int logCount = 0; // Jumlah log yang telah dikumpulkan
    public AudioSource logCollectedAudio; // Audio source untuk log yang dikumpulkan
    public AudioSource logFullAudio; // Audio source untuk jumlah log mencapai 5

    // Fungsi yang dipanggil ketika objek bersentuhan dengan collider lain
    private void OnTriggerEnter(Collider other)
    {
        // Periksa apakah objek yang bersentuhan memiliki tag yang sesuai
        if (other.CompareTag(logTag))
        {
            CollectLog(other.gameObject);
        }
    }

    // Fungsi untuk menambah jumlah log yang telah dikumpulkan
    public void CollectLog(GameObject logObject)
    {
        // Menghancurkan objek log yang disentuh
        Destroy(logObject);

        // Menambah jumlah log yang telah dikumpulkan
        int previousLogCount = logCount;
        logCount++;
        Debug.Log("Log collected. Total: " + logCount);

        // Memainkan audio jika log count bertambah
        if (logCount > previousLogCount && logCollectedAudio != null)
        {
            logCollectedAudio.Play();
        }

        // Jika jumlah log mencapai 5, aktifkan game object tertentu
        if (logCount >= 5)
        {
            ActivateGameObject();

            // Memainkan audio jika jumlah log mencapai 5
            if (logFullAudio != null)
            {
                logFullAudio.Play();
            }
        }
    }

    // Fungsi untuk mengaktifkan game object tertentu
    private void ActivateGameObject()
    {
        if (gameObjectToActivate != null && boundaryDeactive != null)
        {
            gameObjectToActivate.SetActive(true);
            boundaryDeactive.SetActive(false);
            Debug.Log("Game object activated!");
        }
        else
        {
            Debug.LogError("Game object to activate is not set!");
        }
    }
}
