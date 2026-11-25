using UnityEngine;
using UnityEngine.InputSystem;

public class PlagueDoctorInteraction : MonoBehaviour
{
    [SerializeField] private string[] dialogue;
    [SerializeField] private GameObject interactionPanel;
    [SerializeField] private NPCDialogueManager npcDialogueManager;
    [SerializeField] private string question = "Hello, stranger! Do you need help finding your way?";
    [SerializeField] private bool useSimplerDialogue = false;
    [SerializeField] private float interactionDistance = 2.5f;

    private Transform playerTransform;
    private bool playerNearby = false;
    private bool waitForPlayerToLeave = false;

    Player PlayerObject;
   

    private void Update()
    {
        if (playerTransform != null) {
            float distance = Vector3.Distance(transform.position, playerTransform.position);

            if (distance > interactionDistance)
            {
                if (playerNearby)
                {
                    playerNearby = false;
                    waitForPlayerToLeave = false;
                    interactionPanel.SetActive(false);
                    npcDialogueManager.CloseDialogue();

                    if(PlayerObject != null)
                    {
                        PlayerObject.EnableAttack();
                    }
                }
                return;
            } if (!playerNearby)
            {
                playerNearby = true;

                if (!npcDialogueManager.IsDialogueActive)
                    interactionPanel.SetActive(true);
            }
            if (waitForPlayerToLeave && !npcDialogueManager.IsDialogueActive)
            {
                waitForPlayerToLeave = false;
                interactionPanel.SetActive(true);
            }
        }


        if (!playerNearby || Keyboard.current == null)
            return;

        if (!Keyboard.current.eKey.wasPressedThisFrame)
            return;

        if (npcDialogueManager.IsDialogueActive)
        {
            npcDialogueManager.ShowNextInstruction();
        }
        else if (!waitForPlayerToLeave)
        {
            interactionPanel.SetActive(false);

            if (useSimplerDialogue)
            {
                npcDialogueManager.StartSimplerDialogue(dialogue);
            }
            else
            {

                npcDialogueManager.StartDialogue(question, dialogue);
            }
            waitForPlayerToLeave=true;
        }


        
    }
    private void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponentInParent<Player>();
        PlayerObject = player;
        player.DisableAttack();

        if (player != null) 
        {

            playerTransform = player.transform;
            playerNearby = true;

            if (!waitForPlayerToLeave && !npcDialogueManager.IsDialogueActive)
            {
                interactionPanel.SetActive(true);
            }
        }
    }
  
}
