using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class NPCDialogueManager : MonoBehaviour
{
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private GameObject instructionPanel;
    [SerializeField] private TextMeshProUGUI instructionText;
    [SerializeField] private TextMeshProUGUI continueText;
    [SerializeField] private Button yesButton;
    [SerializeField] private Button noButton;

    private string[] currentDialogue;
    private int currentIndex;
    private bool isDialogueActive = false;
    private bool showingInstructions = false;
    public bool IsDialogueActive => isDialogueActive;

    //hides the dialoguebox, connects yes/no-buttons to the methods
    private void Start()
    {
        dialoguePanel.SetActive(false);
        instructionPanel.SetActive(false);

        yesButton.onClick.AddListener(Yes);
        noButton.onClick.AddListener(No);
    }
    // shows plague doctor's question
    public void StartDialogue(string question, string[] instructions)  //for encontering the first plaguedoctor
    {
        if (isDialogueActive) 
            return;

        currentDialogue = instructions;
        currentIndex = 0;
        isDialogueActive= true;
        showingInstructions = false;

        dialoguePanel.SetActive(true);
        dialogueText.text = question;

        yesButton.gameObject.SetActive(true);
        noButton.gameObject.SetActive(true);

    }

    public void StartSimplerDialogue(string[] dialogue) // for encontering plaguedoctor nr 2 and nr 3
    {
        if (isDialogueActive) 
            return;

        currentDialogue = dialogue;
        currentIndex = 0;
        isDialogueActive= true;
        showingInstructions = true;
        dialoguePanel.SetActive(false);
        instructionPanel.SetActive(true);

        ShowNextInstruction();
    }




    // starts the tutorial
    private void Yes()
    {
        showingInstructions= true;
        dialoguePanel.SetActive(false);
        instructionPanel.SetActive(true);

        ShowNextInstruction();
    }
    //closes the dialoguebox
    private void No()
    {
        CloseDialogue();
    }

    // shows next tips/instruction
    public void ShowNextInstruction()
    {
        
        if (!isDialogueActive || !showingInstructions)
            return;

        if (currentDialogue == null || currentIndex >= currentDialogue.Length)
        {
            CloseDialogue();
            return;
        }
        instructionText.text = currentDialogue[currentIndex];
        currentIndex++;

        continueText.text = currentIndex >= currentDialogue.Length
            ? "[E] Close" : "[E] Continue";
    }
    // closes the dialogue
    public void CloseDialogue()
    {
        dialoguePanel.SetActive(false);
        instructionPanel.SetActive(false);
        isDialogueActive = false;
        showingInstructions= false;
      
    }
}

