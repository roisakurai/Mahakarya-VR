using UnityEngine;

public class StoneTrapDoorScript : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void CloseDoor()
    {
        if (animator != null)
        {
            animator.SetTrigger("CloseDoor");
        }
        else
        {
            Debug.LogError("Animator component not found.");
        }
    }
}
