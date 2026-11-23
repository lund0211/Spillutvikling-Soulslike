using UnityEngine;
using System.Collections;

public class TeleportManager : MonoBehaviour
{
    [SerializeField] private CanvasGroup fadePanel;
    [SerializeField] private float fadeDuration = 1f;
    [SerializeField] private Transform boat;
    [SerializeField] private Vector3 boatDestination;
    [SerializeField] private Vector3 boatRotation;
    private Vector3 originalBoatPosition;
    private Quaternion originalBoatRotation;

    private void Awake()
    {
        if (boat != null)
        {
            originalBoatPosition = boat.position;
            originalBoatRotation = boat.rotation;
        }
    }

    public void TeleportPlayer(Player ourplayer, Transform teleportBodyTransform)
    {
        // checks if the player and the teleporting point exists
        if (ourplayer != null && teleportBodyTransform != null)
        {
            Rigidbody playerRB = ourplayer.GetComponent<Rigidbody>();

            if (playerRB != null)
            {
                playerRB.position = teleportBodyTransform.position;
                playerRB.linearVelocity = Vector3.zero;
                playerRB.angularVelocity = Vector3.zero;
            }
            else
            {
                ourplayer.transform.position = teleportBodyTransform.position;
            }
            Debug.Log("Player teleported");
        }
        
    }
    private IEnumerator Fade(float startFading, float endFading)
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;

            fadePanel.alpha = Mathf.Lerp(startFading, endFading, elapsedTime / fadeDuration);
            yield return null;
        }
        fadePanel.alpha = endFading;

    }
    private IEnumerator TeleportSequence(Player ourplayer, Transform teleportBodyTransform, bool returnToVillage)
    {

        //make the screen fade to black

        yield return StartCoroutine(Fade(0f, 1f));

        //teleport the player
        TeleportPlayer(ourplayer, teleportBodyTransform);

        // teleport the boat
        if (boat != null)
        {
            if (returnToVillage)
            {
                boat.position = originalBoatPosition;
                boat.rotation = originalBoatRotation;
            }
            else
            {

                boat.position = boatDestination;
                boat.rotation = Quaternion.Euler(boatRotation);
            }
        }

        // make the screen visible again
        yield return StartCoroutine(Fade(1f, 0f));
    }

    public void StartTeleport(Player ourplayer, Transform teleportBodyTransform, bool returnToVillage)
    
    {
        StartCoroutine(TeleportSequence(ourplayer, teleportBodyTransform, returnToVillage));
    }
}
