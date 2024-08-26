using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectActivatorWithDelay : MonoBehaviour
{
    public UnityEngine.Events.UnityEvent Activated;
    public UnityEngine.Events.UnityEvent Gem;

    public void activator()
    {
        Activated.Invoke();

        // After 5 seconds, activate the Gem game object
        Invoke("ActivateGem", 10f);
    }

    void ActivateGem()
    {
        Gem.Invoke();
    }
}