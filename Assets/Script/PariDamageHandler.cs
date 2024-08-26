using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PariDamageHandler : MonoBehaviour
{
    public int damageAmount = 1;

    private void Start()
    {
        PariHealth.instance.TakeDamage(damageAmount);
    }
}
