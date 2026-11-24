
using UnityEngine;

public class PauseMenuButtons : MonoBehaviour
{
    public void ResumeGame()
    {
        PauseManager manager =
            FindFirstObjectByType<PauseManager>();

        if (manager != null)
        {
            manager.ResumeGame();
        }
    }
}
