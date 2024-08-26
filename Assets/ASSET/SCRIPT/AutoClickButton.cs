using UnityEngine;
using UnityEngine.UI;

public class AutoClickButton : MonoBehaviour
{
    public Button buttonToClick;
    private int clickCount = 3;

    void Start()
    {
        // Check if the button is assigned
        if (buttonToClick != null)
        {
            // Subscribe to the button's onClick event
            buttonToClick.onClick.AddListener(OnClickButton);
            
            // Click the button three times
            while (clickCount < 3)
            {
                buttonToClick.onClick.Invoke();
                clickCount++;
            }
        }
        else
        {
            Debug.LogWarning("Button is not assigned!");
        }
    }

    void OnClickButton()
    {
        Debug.Log("Button clicked!");
    }
}
