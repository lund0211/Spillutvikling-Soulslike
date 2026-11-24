
using UnityEngine;



public class PauseMenuButtons : MonoBehaviour
{

    [SerializeField]
    GameObject PauseMenu;
    [SerializeField]
    GameObject OptionsMenu;
    public void ResumeGame()
    {
        PauseManager manager =
            FindFirstObjectByType<PauseManager>();

        if (manager != null)
        {
            manager.ResumeGame();
        }
    }


    public void Options()
    {
        Debug.Log("Options");
        PauseMenu.SetActive(false);
        OptionsMenu.SetActive(true);
    }

    public void Back()
    {
        PauseMenu.SetActive(true);
        OptionsMenu.SetActive(false);
    }

}
