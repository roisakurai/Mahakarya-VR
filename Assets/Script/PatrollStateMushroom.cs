using UnityEngine.AI;
using System.Collections.Generic;
using UnityEngine;

public class PatrollStateMushroom : StateMachineBehaviour
{
    float timer;
    List<Transform> wayPoints = new List<Transform>();
    List<Transform> wayPoints1 = new List<Transform>();
    List<Transform> wayPoints2 = new List<Transform>();
    NavMeshAgent agent;
    Transform player;
    float chaseRange = 2;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = animator.GetComponent<NavMeshAgent>();
        agent.speed = 1.5f;
        timer = 0;

        // Retrieve waypoints with tag "MushroomPoints"
        GameObject go = GameObject.FindGameObjectWithTag("MushroomPoints");
        if (go != null)
        {
            foreach (Transform t in go.transform)
                wayPoints.Add(t);
        }

        // Retrieve waypoints with tag "MushroomPoints1"
        GameObject go1 = GameObject.FindGameObjectWithTag("MushroomPoints1");
        if (go1 != null)
        {
            foreach (Transform t in go1.transform)
                wayPoints1.Add(t);
        }

        // Retrieve waypoints with tag "BossWolf"
        GameObject go2 = GameObject.FindGameObjectWithTag("BossWolf");
        if (go2 != null)
        {
            foreach (Transform t in go2.transform)
                wayPoints2.Add(t);
        }

        // Set the initial destination based on the selected set of waypoints
        if (wayPoints.Count > 0 || wayPoints1.Count > 0 || wayPoints2.Count > 0)
        {
            int randomIndex = Random.Range(0, 3); // 0 or 1
            if (randomIndex == 0 && wayPoints.Count > 0)
                agent.SetDestination(wayPoints[Random.Range(0, wayPoints.Count)].position);
            else if (randomIndex == 1 && wayPoints1.Count > 0)
                agent.SetDestination(wayPoints1[Random.Range(0, wayPoints1.Count)].position);
            else if (wayPoints2.Count > 0)
                agent.SetDestination(wayPoints2[Random.Range(0, wayPoints2.Count)].position);
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            // Set the destination based on the selected set of waypoints
            int randomIndex = Random.Range(0, 3); // 0 or 1
            if (randomIndex == 0 && wayPoints.Count > 0)
                agent.SetDestination(wayPoints[Random.Range(0, wayPoints.Count)].position);
            else if (randomIndex == 1 && wayPoints1.Count > 0)
                agent.SetDestination(wayPoints1[Random.Range(0, wayPoints1.Count)].position);
            else if (wayPoints2.Count > 0)
                agent.SetDestination(wayPoints2[Random.Range(0, wayPoints2.Count)].position);
        }

        timer += Time.deltaTime;
        if (timer > 10)
            animator.SetBool("isPatrolling", false);

        float distance = Vector3.Distance(player.position, animator.transform.position);
        if (distance < chaseRange)
            animator.SetBool("isShoot", true);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.SetDestination(agent.transform.position);
    }
}
