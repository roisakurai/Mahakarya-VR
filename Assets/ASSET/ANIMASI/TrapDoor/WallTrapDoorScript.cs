using UnityEngine;

public class WallTrapDoorScript : MonoBehaviour
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
            animator.SetTrigger("Close");
        }
        else
        {
            Debug.LogError("Animator component not found.");
        }
    }
}
