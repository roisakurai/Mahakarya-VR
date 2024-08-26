using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeDamageController : MonoBehaviour
{
    public int Damage = 1;

    private void Start()
    {
        BeeHealth.instance.TakeDamage(Damage);
    }
}
