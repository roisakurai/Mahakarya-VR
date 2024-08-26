using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointHandlerDestroy : MonoBehaviour
{
    public int pointsToAdd = 1;

    private void Start()
    {
        PointManager.instance.AddPoints(pointsToAdd);
    }
}