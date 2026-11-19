using UnityEngine;
using System.Collections;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tutorialText;
    [SerializeField] private CanvasGroup tutorialPanel;
    [SerializeField] private float textDuration = 4f;
    [SerializeField] private float instructionDuration = 3f;
    [SerializeField] private float fadeDuration = 0.5f;

   
    private string[] sentences =
    {
        "It's a dark time for Renwick village... ",
        "The Plague Hag has killed almost everyone...",
        "Find the Hag and end her reign of pestilance and terror!"
    };

    private string[] controls =
        {
        "Hold 'SHIFT' to sprint.",
        "Left-Click to attack.",
        "Press 'SPACE' to dodge",
        "Press R to heal.",
        "Press 'E' to interact",
        "If you need to rest, find a bonfire and lit it on fire.",
        "Take the boat to get to the island."

    };

    private IEnumerator Start()
    {
        // show the story introduction
        foreach (string sentence in sentences) 
        {
            tutorialText.text = sentence;

            yield return StartCoroutine(Fade(0f, 1f));
            yield return new WaitForSeconds(textDuration);
            yield return StartCoroutine(Fade(1f, 0f));
        }

        // show the control instrucions
        foreach (string instruction in controls)
        {
            tutorialText.text = instruction;

            yield return StartCoroutine(Fade(0f, 1f));
            yield return new WaitForSeconds(instructionDuration);
            yield return StartCoroutine(Fade(1f, 0f));
        }


        tutorialPanel.gameObject.SetActive(false);
    }
    private IEnumerator Fade(float startAlpha, float endAlpha)
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration) 
        {
            elapsedTime += Time.deltaTime;
            tutorialPanel.alpha = Mathf.Lerp(
                startAlpha, endAlpha, elapsedTime / fadeDuration);
    
            yield return null;
        }
    tutorialPanel.alpha = endAlpha;
    }
}

