using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireActiveForASecond : MonoBehaviour
{
    public GameObject[] Fires;
    public float delayTime = 10f; // Public variable to set the delay time

    void Start()
    {
        StartCoroutine(CheckAndDeactivateFires());
    }

    IEnumerator CheckAndDeactivateFires()
    {
        while (true)
        {
            // Wait for the specified delay time
            yield return new WaitForSeconds(delayTime);

            // Check if all fires are active
            bool allActive = true;
            foreach (GameObject fire in Fires)
            {
                if (fire == null || !fire.activeInHierarchy)
                {
                    allActive = false;
                    break;
                }
            }

            if (allActive)
            {
                // Deactivate all fires
                foreach (GameObject fire in Fires)
                {
                    if (fire != null)
                    {
                        fire.SetActive(false);
                    }
                }

                // Wait for the specified delay time
                yield return new WaitForSeconds(delayTime);

                // Reactivate all fires
                foreach (GameObject fire in Fires)
                {
                    if (fire != null)
                    {
                        fire.SetActive(true);
                    }
                }
            }
        }
    }
}
