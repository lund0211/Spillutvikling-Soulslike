using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class EndOfGameBehavior : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    GameObject VictoryScreen;

    PlagueWitchBehavior witch;

    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShowVictoryScreen()
    {
        VictoryScreen.SetActive(true);
        StartCoroutine(ShowVictoryScreenTimer());
    }

    private IEnumerator ShowVictoryScreenTimer()
    {
        yield return new WaitForSeconds(5);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene("UI_menu");
    }


}
