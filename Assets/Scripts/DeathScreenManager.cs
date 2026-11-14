using UnityEngine;
using System.Collections;
using System.Runtime.CompilerServices;

public class DeathScreenManager : MonoBehaviour
{
    [SerializeField]
    private GameObject deathScreenCanvas;

    [SerializeField]
    private Transform player; // gives us access to the player's position

    [SerializeField]
    private Rigidbody playerRigidbody;

    [SerializeField]
    private CheckpointManager checkpointManager;  //gives us access to the checkpoint-system -> gets the last activated checkpoint position

    [SerializeField]
    private HealthBehavior playerHealth;


    // saves the player's startposition when the game begins
    private void Start()
    {
        startPosition = playerRigidbody.position;
    }

    // displays the death screen when the player dies
    public void ShowDeathScreen()
    {
        if (isRespawning)
        {
            return;
        }
        isRespawning = true;

        deathScreenCanvas.SetActive(true);
        Debug.Log("Death screen displayed");

        StartCoroutine(RespawnAfterDelay());
    }
    // waits 3 seconds before respawning
    private IEnumerator RespawnAfterDelay()
    {
        yield return new WaitForSeconds(3f);

        playerRigidbody.linearVelocity = Vector3.zero;  // resets the player's motion velocity
        playerRigidbody.angularVelocity = Vector3.zero; // resets the rotation velocity

          if (checkpointManager.HasCheckpoint())
           {
              playerRigidbody.position = checkpointManager.GetCheckpointPosition();  // sets the player's position to last activated checkpoint
           }
            else
            {
                playerRigidbody.position = startPosition;
            }

            if (playerHealth != null)
            {
                float missingHealth = playerHealth.GetMaxHealth() - playerHealth.GetHealth();
                playerHealth.AdjustHealthBy(missingHealth);

            }
        // hides the death screen after respawning

            RespawnEnemies();
            deathScreenCanvas.SetActive(false);

            isRespawning = false;
        

    }
    private bool isRespawning = false;

    private Vector3 startPosition;
    private void RespawnEnemies()
    {
        EnemyRespawnBehavior[] enemies =
            FindObjectsByType<EnemyRespawnBehavior>(
                FindObjectsInactive.Include,
                FindObjectsSortMode.None);

        foreach (EnemyRespawnBehavior enemy in enemies)
        {
            enemy.Respawn();
        }
    }


 }



