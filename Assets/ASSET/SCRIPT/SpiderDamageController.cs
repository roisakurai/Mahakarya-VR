using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderDamageController : MonoBehaviour
{
    public int Damage = 1;

    private void Start()
    {
        SpiderHealth.instance.TakeDamage(Damage);
    }
}
