using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerEnterEvent : MonoBehaviour
{

    public string sceneToLoad;
    public AudioClip altarSound;


    private void OnTriggerEnter(Collider other)
    {
        // Mengecek apakah objek yang masuk ke collider adalah pemain
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(ActivateAltarAndLoadScene());
        }
    }

    private IEnumerator ActivateAltarAndLoadScene()
    {
        // Mainkan suara altar
        if (altarSound != null)
        {
            AudioSource.PlayClipAtPoint(altarSound, transform.position);
        }

        // Jika altarSound tidak null, tunggu durasi suaranya. Jika null, tunggu 1 detik.
        if (altarSound != null)
        {
            yield return new WaitForSeconds(altarSound.length);
        }
        else
        {
            yield return new WaitForSeconds(1f);
        }

        // Muat scene berikutnya
        SceneManager.LoadScene(sceneToLoad);
    }
}
