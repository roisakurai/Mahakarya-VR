using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CrabController : MonoBehaviour
{
    public Transform[] waypoints; // Array untuk menyimpan waypoints
    public float patrolSpeed = 2.0f; // Kecepatan patroli
    private Animator animator; // Komponen animator
    private NavMeshAgent navMeshAgent; // Komponen NavMeshAgent
    private int currentWaypointIndex = 0; // Indeks waypoint saat ini
    private bool isDead = false; // Status kepiting mati

    void Start()
    {
        // Mengambil komponen yang diperlukan
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = patrolSpeed; // Mengatur kecepatan patroli
        navMeshAgent.updateRotation = false; // Menonaktifkan rotasi otomatis NavMeshAgent

        // Menentukan tujuan awal jika ada waypoints
        if (waypoints.Length > 0)
        {
            navMeshAgent.SetDestination(waypoints[currentWaypointIndex].position);
        }
    }

    void Update()
    {
        if (isDead) return; // Jika kepiting mati, tidak ada yang dilakukan

        // Melakukan patroli jika ada waypoints
        if (waypoints.Length > 0)
        {
            // Cek apakah kepiting telah mencapai waypoint saat ini
            if (navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance)
            {
                // Update ke waypoint berikutnya
                currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
                navMeshAgent.SetDestination(waypoints[currentWaypointIndex].position);
            }

            // Mengatur animasi berdasarkan pergerakan
            Vector3 direction = navMeshAgent.velocity.normalized;

            if (direction.x > 0)
            {
                animator.SetBool("isWalkingRight", true);
                animator.SetBool("isWalkingLeft", false);
                RotateTowards(Vector3.right);
            }
            else if (direction.x < 0)
            {
                animator.SetBool("isWalkingRight", false);
                animator.SetBool("isWalkingLeft", true);
                RotateTowards(Vector3.left);
            }
            else
            {
                animator.SetBool("isWalkingRight", false);
                animator.SetBool("isWalkingLeft", false);
            }
        }
    }

    void RotateTowards(Vector3 direction)
    {
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, navMeshAgent.angularSpeed * Time.deltaTime);
    }

    public void Die()
    {
        isDead = true;
        animator.SetBool("isDead", true);
        navMeshAgent.isStopped = true; // Menghentikan pergerakan NavMeshAgent
        GetComponent<Collider>().enabled = false; // Opsional: menonaktifkan collider agar kepiting tidak bergerak setelah mati
    }
}