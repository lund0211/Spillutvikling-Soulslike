
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PauseUIDebug : MonoBehaviour
{
    void Update()
    {
        if (Mouse.current != null &&
            Mouse.current.leftButton.wasPressedThisFrame)
        {
            if (EventSystem.current != null)
            {
                Debug.Log("Clicked UI: " +
                    EventSystem.current.IsPointerOverGameObject());
            }
            else
            {
                Debug.Log("No active EventSystem!");
            }
        }
    }
}

