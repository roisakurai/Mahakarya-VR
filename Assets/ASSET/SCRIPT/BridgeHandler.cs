using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeHandler : MonoBehaviour
{
    public int requiredPoints = 5;
    public GameObject bridgeObject;
    public GameObject boundariesObject;

    void Start()
    {
        bridgeObject.SetActive(false);
        boundariesObject.SetActive(true);
    }

    void Update()
    {
        if (BridgePointManager.instance.points >= requiredPoints)
        {
            bridgeObject.SetActive(true);
            boundariesObject.SetActive(false);
            BridgePointManager.instance.AddPoints(-requiredPoints);
        }
    }
}
