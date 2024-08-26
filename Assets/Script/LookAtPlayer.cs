using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    public Transform cam;

    // Update is called once per frame
    void LateUpdate()
    {
        //transform.LookAt(cam);
        Vector3 lookAtDirection = cam.position - transform.position;

        Quaternion rotation = Quaternion.LookRotation(lookAtDirection);

        // Mengaplikasikan rotasi ke objek, dengan menambahkan rotasi tambahan sebesar 180 derajat
        transform.rotation = rotation * Quaternion.Euler(0, 180, 0);
    }
}