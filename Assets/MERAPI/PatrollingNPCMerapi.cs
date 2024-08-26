using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollingNPCMerapi : MonoBehaviour
{
    public Transform[] points;
    int current;
    public float speed;
    public float rotationSpeed = 90f; // Kecepatan rotasi (dalam derajat per detik)

    // Start is called before the first frame update
    void Start()
    {
        current = 0;
    }

    // Update is called once per frame
    void Update()
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
}
