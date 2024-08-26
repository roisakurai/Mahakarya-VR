using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPLayer : MonoBehaviour
{
    public Transform cam;
   
    // Update is called once per frame
    void LateUpdate()
    {
        transform.LookAt(cam);
    }
}
