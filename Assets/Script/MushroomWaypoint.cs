using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomWaypoint : MonoBehaviour
{
    public List<Transform> waypointsList1;
    public List<Transform> waypointsList2;
    public GameObject prefabWaypoint1;
    public GameObject prefabWaypoint2;

    private void Awake()
    {
        // Instantiate prefabs and add their transforms to the waypoint lists
        if (prefabWaypoint1 != null)
        {
            GameObject instance1 = Instantiate(prefabWaypoint1);
            waypointsList1.Add(instance1.transform);
        }

        if (prefabWaypoint2 != null)
        {
            GameObject instance2 = Instantiate(prefabWaypoint2);
            waypointsList2.Add(instance2.transform);
        }
    }

    public List<Transform> GetWaypoints(int listNumber)
    {
        if (listNumber == 1)
            return waypointsList1;
        if (listNumber == 2)
            return waypointsList2;
        return null;
    }
}
