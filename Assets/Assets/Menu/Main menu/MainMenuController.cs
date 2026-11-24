
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System;

public class MainMenuController : MonoBehaviour
{
    [Header("Raven")]
    public RectTransform raven;
    public float ravenOffsetX = 130f;
    public float moveSpeed = 8f;

    [Header("Menu Buttons")]
    public Button[] menuButtons;

    private int selectedIndex = 0;

    void Start()
    {
        Time.timeScale = 1f;

        if (menuButtons.Length > 0)
        {
            SelectButton(0);
            MoveRavenInstantly();
        }
    }

    void Update()
    {
        if (raven == null || menuButtons.Length == 0)
            return;

        if (EventSystem.current != null)
        {
            GameObject selected =
                EventSystem.current.currentSelectedGameObject;

            for (int i = 0; i < menuButtons.Length; i++)
            {
                if (selected == menuButtons[i].gameObject)
                {
                    selectedIndex = i;
                    break;
                }
            }
        }

        Vector3 target = GetRavenTarget();

        raven.position = Vector3.Lerp(
            raven.position,
            target,
            Time.unscaledDeltaTime * moveSpeed
        );
    }

    Vector3 GetRavenTarget()
    {
        RectTransform button =
            menuButtons[selectedIndex].GetComponent<RectTransform>();

        return button.position + Vector3.left * ravenOffsetX;
    }

    void MoveRavenInstantly()
    {
        if (raven != null)
            raven.position = GetRavenTarget();
    }

    public void SelectButton(int index)
    {
        if (index < 0 || index >= menuButtons.Length)
            return;

        selectedIndex = index;

        if (EventSystem.current != null)
        {
            EventSystem.current.SetSelectedGameObject(
                menuButtons[index].gameObject
            );
        }
    }

    public void NewGame()
    {
        SceneManager.LoadScene("VillageScene");
    }

    public void LoadGame()
    {
        Debug.Log("Load Game clicked");
    }

    public void Options()
    {
        Debug.Log("Options clicked");


    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
