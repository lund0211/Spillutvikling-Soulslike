using UnityEngine;
using UnityEngine.InputSystem;

public class CheckpointBehavior : MonoBehaviour
{
    [SerializeField]
    private CheckpointManager checkpointManager;

    [SerializeField]
    private Transform respawnPoint;

    [SerializeField]
    public GameObject FireAnimation;

    private bool playerNearby = false;
    private HealthBehavior playerHealth;

    bool isActivated = false;
    [SerializeField]
    Animator animator;

    [SerializeField]
    GameObject Player;

    Player playerBehavior;

    private Rigidbody PlayerRB;

    EnemyRespawnBehavior[] Enemies;

    private void Awake()
    {
        PlayerRB = Player.GetComponent<Rigidbody>();
        playerBehavior = Player.GetComponent<Player>();
    }

    // detects when the player enters the checkpoint area
    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponentInParent<Player>();

        if (player != null )
        {
            playerNearby = true;
            playerHealth = player.GetComponent<HealthBehavior>();  //finds the health-system the player have

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
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        AnimatorStateInfo stateInfoNext = animator.GetNextAnimatorStateInfo(0);

        if (animator.IsInTransition(0) || (!stateInfo.IsName("Idle") && !stateInfo.IsName("Sitting idle")))
        {
            animator.ResetTrigger("Sitting");
            return;
        }

        if (playerNearby && Keyboard.current != null &&
            Keyboard.current.eKey.wasPressedThisFrame)
        {
            if (isActivated == false)
            {
                isActivated = true;
                Debug.Log("Checkpoint activated");
                FireAnimation.SetActive(true);
                return;
            }
            //saves the position of the activated checkpoint
            checkpointManager.SetCheckpoint(respawnPoint.position);
            animator.SetTrigger("Sitting");
            RespawnAllEnemies();
            playerBehavior.RestorePotionCount();
            playerBehavior.AdjustStamina(playerBehavior.GetMaxStamina());

            // restores the player's health to maximum
            if (playerHealth != null)
            {
                float missingHealth = playerHealth.GetMaxHealth() - playerHealth.GetHealth();
                playerHealth.AdjustHealthBy(missingHealth);
            }


        }
    }

    public void RespawnAllEnemies()
    {
        Enemies = FindObjectsByType<EnemyRespawnBehavior>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        
        foreach(EnemyRespawnBehavior Enemy in Enemies)
        {
            if(Enemy != null)
            {
                Enemy.Respawn();
            }
        }
    }
}
