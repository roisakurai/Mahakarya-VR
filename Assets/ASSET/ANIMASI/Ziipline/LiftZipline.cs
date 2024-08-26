using UnityEngine;

public class LiftZipline : MonoBehaviour
{
    public GameObject parentObject;
    public GameObject childObject;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void TriggerLift()
    {
        if (animator != null)
        {
            animator.SetTrigger("TriggerLift");
            if (parentObject != null && childObject != null)
            {
                childObject.transform.parent = parentObject.transform;
            }
            else
            {
                Debug.LogError("Parent or child GameObjects are not assigned.");
            }
        }
        else
        {
            Debug.LogError("Animator component not found.");
        }
    }
}
