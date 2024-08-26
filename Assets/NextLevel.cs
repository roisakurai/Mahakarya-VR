using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    // Nama scene yang akan dibuka
    public string sceneToLoad;

    // Tambahkan clip suara (opsional)
    public AudioClip altarSound;

    public void GoToNextLevel()
    {
         // Mulai coroutine untuk menunda perubahan scene
        StartCoroutine(ActivateAltarAndLoadScene());
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