using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveForASecond : MonoBehaviour
{
    public GameObject Gas;

    void Start()
    {
        if (Gas || Gas.gameObject.activeInHierarchy) 
        {
            // After 5 seconds, activate the Gem game object
            Invoke("DeactivateObject", 10f);
        }
    }

    void DeactivateObject()
    {
        Gas.SetActive(false);
    }
}
