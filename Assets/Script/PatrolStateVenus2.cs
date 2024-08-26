using UnityEngine.AI;
using System.Collections.Generic;
using UnityEngine;

public class PatrolStateVenus2 : StateMachineBehaviour
{
    public int numberOfWaypoints = 4; // Jumlah waypoint yang ingin Anda buat secara acak
    public float patrolRange = 5f; // Jarak maksimum di mana waypoint dapat dibuat dari posisi awal Venus
    NavMeshAgent agent;

    Transform player;
    float chaseRange = 20;
    float timer; // Deklarasi variabel timer

    List<Vector3> waypoints = new List<Vector3>(); // List untuk menyimpan posisi waypoint acak

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = animator.GetComponent<NavMeshAgent>();
        agent.speed = 1.5f;
        agent.autoBraking = false; // Supaya Venus tidak berhenti mendadak saat mendekati waypoint

        GenerateRandomWaypoints();
        SetNextWaypoint();

        timer = 0; // Inisialisasi timer
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Cek apakah Venus sudah mendekati waypoint, jika sudah cari waypoint berikutnya
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            SetNextWaypoint();
        }

        // Timer untuk mengubah state dari patrolling ke chasing setelah waktu tertentu
        if (timer > 10)
        {
            animator.SetBool("isPatrolling", false);
        }

        timer += Time.deltaTime;

        // Cek jarak antara Venus dengan player, jika lebih kecil dari chaseRange, aktifkan chasing
        float distance = Vector3.Distance(player.position, animator.transform.position);
        if (distance < chaseRange)
        {
            animator.SetBool("isChasing", true);
        }
    }

    void GenerateRandomWaypoints()
    {
        for (int i = 0; i < numberOfWaypoints; i++)
        {
            Vector3 randomPoint = Random.insideUnitSphere * patrolRange;
            randomPoint += agent.transform.position;
            NavMeshHit hit;
            NavMesh.SamplePosition(randomPoint, out hit, patrolRange, NavMesh.AllAreas);
            waypoints.Add(hit.position);
        }
    }

    void SetNextWaypoint()
    {
        int randomIndex = Random.Range(0, waypoints.Count);
        agent.SetDestination(waypoints[randomIndex]);
    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Set destination Venus ke posisi saat ini untuk menghentikannya
        agent.SetDestination(agent.transform.position);
    }
}
