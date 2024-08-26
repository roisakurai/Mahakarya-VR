using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingFox : MonoBehaviour
{
    public Transform startPoint; // Titik awal flying fox
    public Transform endPoint;   // Titik akhir flying fox
    public Transform handle;     // Objek gagang
    public float speed = 5f;     // Kecepatan pergerakan objek pemain

    private bool isMoving = false;
    private Vector3 nextPosition;

    void Start()
    {
        // Mulai pergerakan saat skrip dimulai
        StartMoving();
    }

    void StartMoving()
    {
        // Tentukan titik awal pergerakan
        nextPosition = startPoint.position;
        isMoving = true;
    }

    void Update()
    {
        if (isMoving)
        {
            // Pergerakan objek pemain ke titik berikutnya
            transform.position = Vector3.MoveTowards(transform.position, nextPosition, speed * Time.deltaTime);
            handle.position = transform.position; // Update posisi gagang agar mengikuti pemain

            // Jika objek pemain mencapai titik akhir, hentikan pergerakan
            if (transform.position == endPoint.position)
            {
                isMoving = false;
                Debug.Log("Reached End Point");
                // Lakukan sesuatu ketika objek mencapai titik akhir, misalnya teleportasi kembali ke titik awal
                TeleportToStartPoint();
            }
        }
    }

    void TeleportToStartPoint()
    {
        transform.position = startPoint.position;
        nextPosition = startPoint.position;
        isMoving = true;
    }
}
