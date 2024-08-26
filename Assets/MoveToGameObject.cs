using UnityEngine;
using UnityEngine.AI;

public class MoveToGameObject : MonoBehaviour
{
    public Transform targetObject; // The target public GameObject
    public bool shouldMove = false; // Boolean condition to trigger movement
    public bool isMoving = false; // Boolean to track movement status

    private NavMeshAgent navMeshAgent;
    private Animator animator;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (shouldMove && targetObject != null && !isMoving)
        {
            MoveToTarget();
        }
        else
        {
            // Set the animator to idle when not moving
            animator.SetBool("Run", false);
        }
    }

    private void MoveToTarget()
    {
        // Set the destination to the targetObject
        navMeshAgent.SetDestination(targetObject.position);

        // Check if the agent has reached the destination
        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance && !navMeshAgent.hasPath)
        {
            // Stop moving and set the animator to idle
            shouldMove = false;
            isMoving = false;
            animator.SetBool("Run", false);
        }
        else
        {
            // Set the animator to run while moving
            isMoving = true;
            animator.SetBool("Run", true);
        }
    }
}
