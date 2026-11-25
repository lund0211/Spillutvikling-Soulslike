
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class HugeRatBehavior : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private Vector3 MovementDirection;

    Animator animator;

    NavMeshAgent agent;

    bool HasBeenTriggered = false;

    int encounterCount = 0;

    private bool playerNearby = false;

    [SerializeField] private GameObject interactionPanel;
    [SerializeField] private NPCDialogueManager npcDialogueManager;
    [SerializeField] private string[] dialogue;

    private bool waitForPlayerToLeave = false;

    void Start()
    {

        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

    }

    // Update is called once per frame
    void Update()
    {
        if (encounterCount == 1)
        {
            if (!HasBeenTriggered || agent.pathPending || !agent.hasPath)
            {
                return;
            }

            if (agent.remainingDistance <= 2)
            {
                Teleport();
            }
        }
        if (encounterCount == 2)
        {
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
                npcDialogueManager.StartSimplerDialogue(dialogue);
                waitForPlayerToLeave = true;
            }
        }
       
    }

    public void EnableMovement()
    {
        if (encounterCount != 0)
            return;

        agent.SetDestination(new Vector3(-93.1900024f, 1.13999999f, 34.2299995f));
        HasBeenTriggered = true;
        animator.SetTrigger("Walking");
        encounterCount++;
    }
    private void Teleport()
    {
        agent.ResetPath();
        agent.Warp(new Vector3(57.1100006f, 23.5400009f, 386.660004f));
        transform.rotation = Quaternion.Euler(0, 199.504822f, 0);
        animator.SetTrigger("Idle");
        encounterCount++;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && encounterCount == 2)
        {
            playerNearby = true;

            if (!waitForPlayerToLeave && !npcDialogueManager.IsDialogueActive)
            {
                interactionPanel.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && playerNearby)
        {
            playerNearby = false;
            interactionPanel.SetActive(false);

            if (waitForPlayerToLeave && npcDialogueManager.IsDialogueActive)
                npcDialogueManager.CloseDialogue();
            waitForPlayerToLeave = false;
            
        }
    }
}
