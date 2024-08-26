using UnityEngine;

public class FireActivator : MonoBehaviour
{
    public GameObject[] objectsToEnable;
    private bool[] objectStates;

    void Update()
    {
        objectStates = new bool[objectsToEnable.Length];
        for (int i = 0; i < objectsToEnable.Length; i++)
        {
            objectStates[i] = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fire"))
        {
            EnableObjects();
        }
    }

    void EnableObjects()
    {
        for (int i = 0; i < objectsToEnable.Length; i++)
        {
            if (objectsToEnable[i] != null && !objectStates[i])
            {
                objectsToEnable[i].SetActive(true);
                objectStates[i] = true;
                Debug.Log("Torch Enabled: " + objectsToEnable[i].name);
            }
        }
    }
}
