using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinnerActivator : MonoBehaviour
{
    public GameObject Boss;
    public UnityEngine.Events.UnityEvent Activated;

    void Update()
    {
        if (!Boss || !Boss.gameObject.activeInHierarchy)
        {
            Activated.Invoke();
        }
    }
}
