using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfDamageController : MonoBehaviour
{
    public int Damage = 1;

    private void Start()
    {
        WolfHealth.instance.TakeDamage(Damage);
    }
}
