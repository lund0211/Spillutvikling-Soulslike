
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PauseManager : MonoBehaviour
{
    public static bool GameIsPaused = false;

    [SerializeField]
    private HideCursor hideCursor;

    [SerializeField]
    private PlayerInput playerInput;

    [SerializeField]
    private string pauseSceneName = "PauseMenu";

    private bool isTransitioning = false;

    void Update()
    {
        if (Keyboard.current != null &&
            Keyboard.current.escapeKey.wasPressedThisFrame &&
            !isTransitioning)
        {
            if (GameIsPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        if (GameIsPaused || isTransitioning) return;

        GameIsPaused = true;

        if (playerInput != null)
        {
            playerInput.DeactivateInput();
        }

        if (hideCursor != null)
        {
            hideCursor.UnlockCursor();
        }

        Time.timeScale = 0f;

        StartCoroutine(LoadPauseMenu());
    }

    
    private System.Collections.IEnumerator LoadPauseMenu()
    {
        isTransitioning = true;

        AsyncOperation operation = SceneManager.LoadSceneAsync(
            pauseSceneName,
            LoadSceneMode.Additive
        );

        if (operation != null)
        {
            while (!operation.isDone)
                yield return null;
        }

        isTransitioning = false;
    }


    public void ResumeGame()
    {
        if (!GameIsPaused || isTransitioning) return;

        StartCoroutine(UnloadPauseMenu());
    }

    private System.Collections.IEnumerator UnloadPauseMenu()
    {
        isTransitioning = true;

        AsyncOperation operation =
            SceneManager.UnloadSceneAsync(pauseSceneName);

        if (operation != null)
        {
            while (!operation.isDone)
                yield return null;
        }

        Time.timeScale = 1f;

        if (playerInput != null)
        {
            playerInput.ActivateInput();
        }

        if (hideCursor != null)
        {
            hideCursor.LockCursor();
        }

        GameIsPaused = false;
        isTransitioning = false;
    }

    private void OnDestroy()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
}
