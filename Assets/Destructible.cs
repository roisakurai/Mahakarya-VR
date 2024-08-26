using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(AudioSource))]
public class Destructible : MonoBehaviour
{
    private int currentHP;
    private bool isDestroyed = false;

    public int MaxHP = 1; // Jumlah HP maksimal

    public GameObject Gas;
    public GameObject Point;
    public GameObject Bubble;


    private Rigidbody Rigidbody;
    private AudioSource AudioSource;
    [SerializeField]
    private GameObject BrokenPrefab;
    [SerializeField]
    private AudioClip DestructionClip;
    [SerializeField]
    private float ExplosiveForce = 10;
    [SerializeField]
    private float ExplosiveRadius = 2;
    [SerializeField]
    public float PieceFadeSpeed = 0f;
    [SerializeField]
    public float PieceDestroyDelay = 0f;
    [SerializeField]
    public float PieceSleepCheckDelay = 0f;

    private int addPoints = 5;

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        currentHP = MaxHP; // Set HP awal saat inisialisasi
        AudioSource = GetComponent<AudioSource>();
    }

    public void Explode()
    {
        Destroy(Rigidbody);
        GetComponent<Collider>().enabled = false;
        GetComponent<Renderer>().enabled = false;
        /*Score.instance.AddScore(addPoints);*/
        if (Gas != null)
        {
            Gas.SetActive(true);
            StartCoroutine(FadeOutGas());
        }

        if (Bubble != null)
        {
            StartCoroutine(DisableBubbleAfterDelay(2.0f)); // Menonaktifkan Bubble setelah 2 detik
        }


        if (DestructionClip != null)
        {
            AudioSource.PlayOneShot(DestructionClip);
        }
        /*Debug.Log("POSISI", btransform.position.x);*/
        GameObject brokenInstance = Instantiate(BrokenPrefab, transform.position, transform.rotation);

        Rigidbody[] rigidbodies = brokenInstance.GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody body in rigidbodies)
        {
            if (Rigidbody != null)
            {
                // inherit velocities
                body.velocity = Rigidbody.velocity;
            }
            body.AddExplosionForce(ExplosiveForce, transform.position, ExplosiveRadius);
        }

        StartCoroutine(FadeOutRigidBodies(rigidbodies));
    }

    private IEnumerator FadeOutGas()
    {
        yield return new WaitForSeconds(PieceDestroyDelay);

        float time = 0;

        Renderer gasRenderer = Gas.GetComponent<Renderer>();

        while (time < 1)
        {
            float step = Time.deltaTime * PieceFadeSpeed;
            gasRenderer.transform.Translate(Vector3.down * (step / gasRenderer.bounds.size.y), Space.World);

            time += step;
            yield return null;
        }

        Destroy(Gas);
    }

    public void TakeDamage(int amount)
    {
        if (isDestroyed) return;

        currentHP -= amount;

        if (currentHP <= 0)
        {
            isDestroyed = true;
            Point.SetActive(true);
            Explode();
        }
        else
        {
            // Play a sound or show an effect for taking damage
        }
    }

    public void TakeDamageBatu(int amount)
    {
        if (isDestroyed) return;

        currentHP -= amount;

        if (currentHP <= 0)
        {
            isDestroyed = true;
            Explode();
        }
        else
        {
            // Play a sound or show an effect for taking damage
        }
    }

    public void DestroyByWeapon()
    {
        Explode();
    }


    private IEnumerator FadeOutRigidBodies(Rigidbody[] Rigidbodies)
    {
        WaitForSeconds Wait = new WaitForSeconds(PieceSleepCheckDelay);
        float activeRigidbodies = Rigidbodies.Length;

        while (activeRigidbodies > 0)
        {
            yield return Wait;

            foreach (Rigidbody rigidbody in Rigidbodies)
            {
                if (rigidbody.IsSleeping())
                {
                    activeRigidbodies--;
                }
            }
        }


        yield return new WaitForSeconds(PieceDestroyDelay);

        float time = 0;
        Renderer[] renderers = Array.ConvertAll(Rigidbodies, GetRendererFromRigidbody);

        foreach (Rigidbody body in Rigidbodies)
        {
            Destroy(body.GetComponent<Collider>());
            Destroy(body);
        }

        while (time < 1)
        {
            float step = Time.deltaTime * PieceFadeSpeed;
            foreach (Renderer renderer in renderers)
            {
                renderer.transform.Translate(Vector3.down * (step / renderer.bounds.size.y), Space.World);
            }

            time += step;
            yield return null;
        }

        foreach (Renderer renderer in renderers)
        {
            Destroy(renderer.gameObject);
        }
        Destroy(gameObject);
    }

    private Renderer GetRendererFromRigidbody(Rigidbody Rigidbody)
    {
        return Rigidbody.GetComponent<Renderer>();
    }

    private IEnumerator DisableBubbleAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Bubble.SetActive(false);
    }
}