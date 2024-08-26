using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    public void TakeItem()
    {
        ItemCollectorCount.instance.TakeDamage(1);
        // Destroy this game object
        Destroy(gameObject);

    }
}
