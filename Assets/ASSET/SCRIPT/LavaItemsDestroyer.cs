using UnityEngine;

public class LavaItemsDestroyer : MonoBehaviour
{
    public GameObject item1; // Game object to check collision with
    public GameObject item2; // Game object to check collision with
    public GameObject item3; // Game object to check collision with
    public GameObject item4; // Game object to check collision with
    public UnityEngine.Events.UnityEvent onCollisionEvent; // Event to trigger on collision

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == item1 || collision.gameObject == item2 || collision.gameObject == item3 || collision.gameObject == item4)
        {
            // Trigger the event when the target object collides with the floor
            onCollisionEvent.Invoke();
            
            // Deactivate the collided game object
            collision.gameObject.SetActive(false);
        }
    }
}
