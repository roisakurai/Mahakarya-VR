using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonDamageController : MonoBehaviour
{
    public int damageAmount = 1;

    public void DealDamage()
    {
        DragonHealth.instance.TakeDamage(damageAmount);
    }
}

