using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public string playerTag = "Player"; // Tag for player objects
    public float chaseRadius = 10f;
    public float stopRadius = 5f;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 2f;

    private Transform playerTransform; // Reference to player transform
    private float nextFireTime;

    void Start()
    {
        // Find player object using tag
        GameObject playerObject = GameObject.FindGameObjectWithTag(playerTag);
        if (playerObject != null)
        {
            playerTransform = playerObject.transform;
        }
        else
        {
            Debug.LogError("Player object not found with tag: " + playerTag);
        }
    }

    void Update()
    {
        if (playerTransform == null)
        {
            return; // Don't proceed if player transform is not found
        }

        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer <= chaseRadius && distanceToPlayer > stopRadius)
        {
            // Chase the player
            transform.LookAt(playerTransform);
            transform.Translate(Vector3.forward * Time.deltaTime);
        }
        else if (distanceToPlayer <= stopRadius)
        {
            // Stop chasing when within stopRadius
            // You can perform any other action here, like attacking
            if (Time.time >= nextFireTime)
            {
                Shoot();
                nextFireTime = Time.time + 1f / fireRate;
            }
        }
    }

    void Shoot()
    {
        // Calculate direction to player
        Vector3 direction = (playerTransform.position - firePoint.position).normalized;

        // Set bullet rotation to align with calculated direction
        Quaternion bulletRotation = Quaternion.LookRotation(direction);

        // Instantiate bullet with fixed rotation
        Instantiate(bulletPrefab, firePoint.position, bulletRotation);
    }
}