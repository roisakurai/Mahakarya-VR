using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantDamageController : MonoBehaviour
{
    public int Damage = 1;

    private void Start()
    {
        PlantDionaneaHealth.instance.TakeDamage(Damage);
    }
}
