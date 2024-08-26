using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorobudurBossGolemDamage : MonoBehaviour
{
    public int Damage = 1;

    private void Start()
    {
        GolemHealth.instance.TakeDamage(Damage);
    }
}
