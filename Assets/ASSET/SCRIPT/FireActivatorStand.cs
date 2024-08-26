using UnityEngine;

public class FireActivatorStand : MonoBehaviour
{
    public GameObject objectReferenceToEnable;
    public GameObject objectWantToEnable;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fire") && other.gameObject == objectReferenceToEnable)
        {
            EnableObject();
        }
    }

    void EnableObject()
    {
        if (objectWantToEnable != null)
        {
            objectWantToEnable.SetActive(true);

            // Optionally, you can disable the collider to prevent multiple activations
            // GetComponent<Collider>().enabled = false;
        }
    }
}
