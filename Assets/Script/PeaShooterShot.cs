using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeaShooterShot : MonoBehaviour
{
    private GameObject mEnemy;
    Transform _Player;
    float dist;
    public float howClose;
    public Transform head, mouth;
    public GameObject _projectile;
    public float fireRate, nextFire;
    private Animator animator;

    public float playDistance = 5f;

    void Start()
    {

        animator = GetComponent<Animator>();
        _Player = GameObject.FindGameObjectWithTag("Player").transform;

    }

    // Update is called once per frame
    void Update()
    {
        // Hitung jarak antara musuh dan pemain
        float distanceToPlayer = Vector3.Distance(transform.position, _Player.position);

        // Atur animasi berdasarkan jarak
        if (distanceToPlayer < playDistance)
        {
            animator.SetBool("shoot", true);
            dist = Vector3.Distance(_Player.position, transform.position);
            if (dist <= howClose)
            {
                head.LookAt(_Player);
                if (Time.time >= nextFire)
                {
                    nextFire = Time.time + 1f / fireRate;
                    shoot();
                }
            }
        }
        else
        {
            animator.SetBool("shoot", false);
        }

        /*dist = Vector3.Distance(_Player.position, transform.position);
        if(dist <= howClose)
        {
            head.LookAt(_Player);
            if(Time.time >= nextFire)
            {
                nextFire = Time.time + 1f / fireRate;
                shoot();
            }
        }*/
    }

    void shoot()
    {
        GameObject clone = Instantiate(_projectile, mouth.position, transform.rotation);
        clone.GetComponent<Rigidbody>().AddForce(transform.forward * 600);
        Destroy(clone, 3);
    }
}
