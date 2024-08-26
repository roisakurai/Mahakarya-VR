using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossNaga : MonoBehaviour
{
    public Transform cam;
    public Transform[] points;
    int current;
    public float speed;
    public float rotationSpeed = 90f; // Kecepatan rotasi (dalam derajat per detik)
    public float chaseRadius = 7f;
    public float attackRadius = 2f; // Jarak saat animasi Fly Fire Breath Attack Low diaktifkan
    public string playerTag = "Player";
    public Animator animator; // Assign Animator component through Unity Editor

    // Start is called before the first frame update
    void Start()
    {
        current = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // Jika ada pemain dalam radius, lakukan pengejaran
        Collider[] colliders = Physics.OverlapSphere(transform.position, chaseRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag(playerTag))
            {
                ChasePlayer(collider.transform.position);
                // Cek jarak dan aktifkan atau nonaktifkan animasi sesuai kondisi
                float distanceToPlayer = Vector3.Distance(transform.position, collider.transform.position);
                if (distanceToPlayer <= attackRadius)
                {
                    animator.SetBool("FlyFireBreathAttackLow", true);
                    transform.LookAt(cam);
                }
                else
                {
                    animator.SetBool("FlyFireBreathAttackLow", false);
                }
                return; // Keluar dari Update jika ada pemain dalam radius
            }
        }

        // Jika tidak ada pemain dalam radius, lanjutkan dengan patroli
        Patrol();
    }

    void Patrol()
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

        // Nonaktifkan animasi saat sedang berpatroli
        animator.SetBool("FlyFireBreathAttackLow", false);
    }

    void ChasePlayer(Vector3 playerPosition)
    {
        float distanceToPlayer = Vector3.Distance(transform.position, playerPosition);

        // Hanya mengejar pemain jika berjarak minimal 3f
        if (distanceToPlayer >= 7f)
        {
            // Pindahkan objek ke arah pemain
            transform.position = Vector3.MoveTowards(transform.position, playerPosition, speed * Time.deltaTime);

            // Rotasi objek setiap frame
            Quaternion targetRotation = Quaternion.LookRotation(playerPosition - transform.position);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        // Nonaktifkan animasi saat sedang mengejar pemain
        animator.SetBool("FlyFireBreathAttackLow", false);
    }

}
