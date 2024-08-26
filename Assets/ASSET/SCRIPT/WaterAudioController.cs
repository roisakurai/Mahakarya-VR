using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterAudioController : MonoBehaviour
{
    public Transform player; // Referensi ke transform pemain
    private AudioSource audioSource;
    public float maxDistance = 5f; // Jarak maksimum di mana suara masih terdengar

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);
        if (distance < maxDistance)
        {
            // Mengatur volume berdasarkan jarak
            audioSource.volume = 1 - (distance / maxDistance);
        }
        else
        {
            // Jika jarak lebih besar dari maxDistance, volume 0
            audioSource.volume = 0;
        }
    }
}