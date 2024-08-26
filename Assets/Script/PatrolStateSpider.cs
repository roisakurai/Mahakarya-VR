using UnityEngine.AI;
using System.Collections.Generic;
using UnityEngine;

public class PatrolStateSpider : StateMachineBehaviour
{
    float timer;
    List<Transform> wayPoints = new List<Transform>();
    List<Transform> wayPoints1 = new List<Transform>();
    List<Transform> wayPoints2 = new List<Transform>();
    List<Transform> wayPoints3 = new List<Transform>();
    List<Transform> wayPoints4 = new List<Transform>(); // New list for waypoints with tag "WayPoints4"
    NavMeshAgent agent;

    Transform player;
    float chaseRange = 8;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = animator.GetComponent<NavMeshAgent>();
        agent.speed = 1.5f;
        timer = 0;

        // Retrieve waypoints with tag "WayPoints"
        GameObject go = GameObject.FindGameObjectWithTag("WayPoints");
        foreach (Transform t in go.transform)
            wayPoints.Add(t);

        // Retrieve waypoints with tag "WayPoints1"
        GameObject go1 = GameObject.FindGameObjectWithTag("WayPoints1");
        foreach (Transform t in go1.transform)
            wayPoints1.Add(t);

        // Retrieve waypoints with tag "WayPoints2"
        GameObject go2 = GameObject.FindGameObjectWithTag("WayPoints2");
        foreach (Transform t in go2.transform)
            wayPoints2.Add(t);

        // Retrieve waypoints with tag "WayPoints3"
        GameObject go3 = GameObject.FindGameObjectWithTag("WayPoints3");
        foreach (Transform t in go3.transform)
            wayPoints3.Add(t);

        // Retrieve waypoints with tag "WayPoints4"
        GameObject go4 = GameObject.FindGameObjectWithTag("WayPoints4");
        foreach (Transform t in go4.transform)
            wayPoints4.Add(t);

        // Set the initial destination based on the selected set of waypoints
        int randomIndex = Random.Range(0, 5); // 0, 1, 2, 3, or 4
        if (randomIndex == 0)
            agent.SetDestination(wayPoints[Random.Range(0, wayPoints.Count)].position);
        else if (randomIndex == 1)
            agent.SetDestination(wayPoints1[Random.Range(0, wayPoints1.Count)].position);
        else if (randomIndex == 2)
            agent.SetDestination(wayPoints2[Random.Range(0, wayPoints2.Count)].position);
        else if (randomIndex == 3)
            agent.SetDestination(wayPoints3[Random.Range(0, wayPoints3.Count)].position);
        else
            agent.SetDestination(wayPoints4[Random.Range(0, wayPoints4.Count)].position);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            // Set the destination based on the selected set of waypoints
            int randomIndex = Random.Range(0, 5); // 0, 1, 2, 3, or 4
            if (randomIndex == 0)
                agent.SetDestination(wayPoints[Random.Range(0, wayPoints.Count)].position);
            else if (randomIndex == 1)
                agent.SetDestination(wayPoints1[Random.Range(0, wayPoints1.Count)].position);
            else if (randomIndex == 2)
                agent.SetDestination(wayPoints2[Random.Range(0, wayPoints2.Count)].position);
            else if (randomIndex == 3)
                agent.SetDestination(wayPoints3[Random.Range(0, wayPoints3.Count)].position);
            else
                agent.SetDestination(wayPoints4[Random.Range(0, wayPoints4.Count)].position);
        }

        timer += Time.deltaTime;
        if (timer > 10)
            animator.SetBool("isPatrolling", false);

        float distance = Vector3.Distance(player.position, animator.transform.position);
        if (distance < chaseRange)
            animator.SetBool("isChasing", true);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.SetDestination(agent.transform.position);
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
