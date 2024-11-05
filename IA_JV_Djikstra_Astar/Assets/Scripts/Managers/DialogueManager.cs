using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    [System.Serializable]
    public struct DialogueEntry
    {
        public string characterName;
        public Sprite characterPortrait;
        [TextArea(3, 10)]
        public string dialogueLine;
    }

    public DialogueEntry[] dialogueLines;

    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI characterNameText;
    public Image characterPortraitImage;
    public GameObject dialogueUI;
    public GameObject controlScreen;

    private int currentLineIndex = 0;
    public float letterDelay = 0.05f;

    private bool isTyping = false;
    private bool tuto_show = false;

    void Start()
    {
        Time.timeScale = 0f; // Pause the game at the beginning
        dialogueUI.SetActive(true);
        controlScreen.SetActive(false);
        StartCoroutine(TypeLine(dialogueLines[currentLineIndex]));
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !tuto_show)
        {

            if (!isTyping && !controlScreen.activeSelf)
            {
                currentLineIndex++;
                if (currentLineIndex < dialogueLines.Length)
                {
                    StartCoroutine(TypeLine(dialogueLines[currentLineIndex]));
                }
                else
                {
                    ShowControlScreen();
                }
            }
            else if (controlScreen.activeSelf)
            {
                tuto_show = true;
                StartLevel();
            }
            
        }
    }

    IEnumerator TypeLine(DialogueEntry line)
    {
        isTyping = true;

        // Set the character name and portrait
        characterNameText.text = line.characterName;
        characterPortraitImage.sprite = line.characterPortrait;

        dialogueText.text = ""; // Clear existing text

        foreach (char letter in line.dialogueLine.ToCharArray())
        {
            dialogueText.text += letter; // Add each letter one by one
            yield return new WaitForSecondsRealtime(letterDelay); // Use WaitForSecondsRealtime to ignore time scale
        }

        isTyping = false;
    }

    void ShowControlScreen()
    {
        dialogueUI.SetActive(false); // Hide the dialogue UI
        controlScreen.SetActive(true); // Show the control screen
    }

    void StartLevel()
    {
        controlScreen.SetActive(false); // Hide the control screen
        Time.timeScale = 1f; // Resume the game once the dialogue is over
    }
}
