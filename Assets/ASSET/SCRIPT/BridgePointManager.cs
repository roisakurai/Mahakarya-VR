using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgePointManager : MonoBehaviour
{
    public static BridgePointManager instance;

    public int points = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddPoints(int amount)
    {
        points += amount;
    }
}