using UnityEngine;

// this script is for keeping track of the last activated checkpoint
public class CheckpointManager : MonoBehaviour
{
    private Vector3 checkpointPosition;
    private bool hasCheckpoint = false;

    // saves the position of the newly activated checkpoint
    public void SetCheckpoint(Vector3 position)
    {
        checkpointPosition = position;
        hasCheckpoint = true;

        Debug.Log("Checkpoint position saved: " + checkpointPosition);
    }

    // returns the position of the last activated checkpoint
    public Vector3 GetCheckpointPosition()
    {
        return checkpointPosition;
    }

    // checks if a checkpoint has been activated
    public bool HasCheckpoint()
    {
        return hasCheckpoint;
    }
}
