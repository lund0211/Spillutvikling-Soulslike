using UnityEngine;
using UnityEngine.InputSystem;

public class TeleportBehavior : MonoBehaviour
{
    [SerializeField] private Transform teleportBodyTransform;
    [SerializeField] private TeleportManager teleportManager;
    [SerializeField] private GameObject teleportMenyPrompt;
    private Player playerNearby;
   
    // register that player walks within trigger area close to the ship
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("someone entered the ship trigger: " + other.name);

        Player player = other.GetComponentInParent<Player>();
            if (player != null )
        {
            playerNearby = player;
            teleportMenyPrompt.SetActive(true);
            Debug.Log("Press E to teleport");
        }
        
    }
    // register that player leaves trigger area
    private void OnTriggerExit(Collider other)
    {
        Player player = other.GetComponentInParent<Player>();
        if (playerNearby != null && player == playerNearby)
        {
            playerNearby = null;
            teleportMenyPrompt.SetActive(false);
        }
        
    }

    private void Update()
    {
        if (playerNearby != null && Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
        {
            teleportManager.StartTeleport(playerNearby, teleportBodyTransform);
        }
    }
}
