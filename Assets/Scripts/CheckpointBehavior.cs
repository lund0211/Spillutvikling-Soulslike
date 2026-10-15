using UnityEngine;
using UnityEngine.InputSystem;

public class CheckpointBehavior : MonoBehaviour
{
    [SerializeField]
    private CheckpointManager checkpointManager;

    private bool playerNearby = false;

    // detects when the player enters the checkpoint area
    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponentInParent<Player>();

        if (player != null )
        {
            playerNearby = true;
            Debug.Log("Press E to activate checkpoint");
        }
    }

    // detects when the player leaves the checkpoint area
    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponentInParent<Player>();
        if (player != null)
        {
            playerNearby = false;
        }
    }

    // checks if the player is nearby and presses E to activate the checkpoint
    private void Update()
    {
        if (playerNearby && Keyboard.current != null &&
            Keyboard.current.eKey.wasPressedThisFrame)
        {
            //saves the position of the activated checkpoint
            checkpointManager.SetCheckpoint(transform.position);
            Debug.Log("Checkpoint activated");
        }
    }
}
