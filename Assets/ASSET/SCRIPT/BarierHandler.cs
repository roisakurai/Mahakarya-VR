using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarierHandler : MonoBehaviour
{
    public int requiredPoints = 10;

    private void OnTriggerEnter(Collider other)
    {
        if (PointManager.instance.points >= requiredPoints)
        {
            gameObject.SetActive(false);
            PointManager.instance.AddPoints(-requiredPoints);
        }
    }
}
