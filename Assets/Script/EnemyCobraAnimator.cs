using System.Collections;
using UnityEngine;

public class EnemyCobraAnimator : MonoBehaviour
{
    public Animator musuhAnimator;
    public float delayBeforeDie = 1.5f; // Tambahkan variabel delay

    void Start()
    {
        // Mendapatkan komponen Animator dari musuh
        musuhAnimator = GetComponent<Animator>();

        // Memastikan musuh memiliki komponen Animator sebelum digunakan
        if (musuhAnimator == null)
        {
            Debug.LogError("Animator component not found on the enemy.");
        }
    }

    // Fungsi untuk memainkan animasi dengan delay sebelum menghancurkan musuh
    public void PlayDestroyAnimationAndDestroy()
    {
        if (musuhAnimator != null)
        {
            // Menunggu sebentar sebelum memainkan animasi "Die"
            Invoke("PlayDestroyAnimation", delayBeforeDie);
        }
        else
        {
            Debug.LogError("Animator component not found on the enemy.");
        }
    }

    void PlayDestroyAnimation()
    {
        // Memicu animasi dengan parameter "Die"
        musuhAnimator.SetTrigger("Die");

        // Menunggu sebentar sebelum menghancurkan objek
        float destroyDelay = musuhAnimator.GetCurrentAnimatorClipInfo(0).Length;
        Invoke("DestroyObject", destroyDelay);
    }

    void DestroyObject()
    {
        // Menghancurkan musuh setelah animasi selesai
        Destroy(gameObject);
    }
}
