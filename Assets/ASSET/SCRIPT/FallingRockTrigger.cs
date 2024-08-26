using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingRockTrigger : MonoBehaviour
{
    public UnityEngine.Events.UnityEvent onCollisionEvent; // Event to trigger on collision

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Trigger the event when the target object collides with the floor
            onCollisionEvent.Invoke();
        }
    }
}
