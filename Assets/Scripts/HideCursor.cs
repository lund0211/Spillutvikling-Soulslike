
using Unity.Cinemachine;
using UnityEngine;

public class HideCursor : MonoBehaviour
{
    [SerializeField] private CinemachineInputAxisController cameraInput;

    void Start()
    {
        LockCursor();
    }

    public void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        cameraInput.enabled = true;
    }

    public void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        cameraInput.enabled = false;
    }
}
