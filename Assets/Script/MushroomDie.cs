using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomDie : MonoBehaviour
{
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider trigger)
    {
        if (trigger.CompareTag("Gas"))
        {
            animator.SetTrigger("die");
            // Panggil fungsi DestroyObject setelah 2 detik
            Invoke("DestroyObject", 2f);
        }
    }

    void DestroyObject()
    {
        // Hancurkan objek saat fungsi ini dipanggil
        Destroy(gameObject);
    }
}
