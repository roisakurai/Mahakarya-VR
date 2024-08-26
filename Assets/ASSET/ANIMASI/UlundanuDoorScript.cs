using UnityEngine;

public class UlundanuDoorScript : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Open()
    {
        if (animator != null)
        {
            animator.SetTrigger("Open");
        }
        else
        {
            Debug.LogError("Animator component not found.");
        }
    }
}
