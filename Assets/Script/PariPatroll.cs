using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PariPatroll : MonoBehaviour
{
    public Transform[] points;
    int current;
    public float speed;
    public float rotationSpeed = 90f; // Kecepatan rotasi (dalam derajat per detik)

    public GameObject bulletPrefab; // Prefab peluru
    public Transform bulletSpawnPoint; // Posisi spawn peluru
    public GameObject explosionPrefab; // Prefab ledakan

    private void Start()
    {
        current = 0;
        StartCoroutine(DropBulletRoutine()); // Mulai coroutine untuk menjatuhkan peluru
    }

    private void Update()
    {
        if (transform.position != points[current].position)
        {
            // Pindahkan objek ke titik patroli
            transform.position = Vector3.MoveTowards(transform.position, points[current].position, speed * Time.deltaTime);

            // Rotasi objek setiap frame
            Quaternion targetRotation = Quaternion.LookRotation(points[current].position - transform.position);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
        else
        {
            // Ganti ke titik patroli berikutnya
            current = (current + 1) % points.Length;
        }
    }

    private IEnumerator DropBulletRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f); // Tunggu 5 detik

            // Menjatuhkan peluru
            DropBullet();
        }
    }

    private void DropBullet()
    {
        // Buat instance peluru pada posisi spawn point
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        // Set prefab ledakan pada peluru
        bullet.GetComponent<PariBullet>().explosionPrefab = explosionPrefab;
    }
}
