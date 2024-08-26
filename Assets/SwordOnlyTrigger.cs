using UnityEngine;

public class SwordOnlyTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger has the tag "Sword"
        if (other.CompareTag("Sword"))
        {
            // Allow the object with the tag "Sword" to pass through
            Debug.Log("Sword passed through the trigger.");
        }
        else
        {
            // If the object doesn't have the tag "Sword", disable its collider
            other.isTrigger = false;
            Debug.Log("Only objects with the tag 'Sword' can pass through.");
        }
    }
}
