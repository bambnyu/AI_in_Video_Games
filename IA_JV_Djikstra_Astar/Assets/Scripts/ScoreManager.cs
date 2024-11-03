using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance; //  create an instance of the ScoreManager class to allow other scripts to access it

    public TextMeshProUGUI scoreText; // Reference to the TextMeshPro score text 
    private int score = 0; // Initial score
    public int scoreToNextLevel = 5; // The score required to trigger the victory screen

    //private VictoryManager victoryManager; // Reference to the VictoryManager
    [SerializeField] private VictoryManager victoryManager; // Use SerializeField to assign it manually

    void Awake()
    {
        // Ensure only one instance of ScoreManager exists 
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        UpdateScoreText(); // Initialize score display
        //victoryManager = FindObjectOfType<VictoryManager>(); // Find the VictoryManager in the scene

        // Add this check to log an error if VictoryManager is not found
        if (victoryManager == null)
        {
            Debug.LogError("VictoryManager not found in the scene. Make sure it's attached to the VictoryCanvas.");
        }
    }

    public void AddScore(int amount)
    {
        score += amount; // Increase score
        UpdateScoreText(); // Update the score display

        // Check if the score has reached the threshold to show the victory screen
        if (score >= scoreToNextLevel)
        {
            TriggerVictory();
        }
    }

    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + score; // Update the TextMeshPro UI text
    }

    private void TriggerVictory()
    {
        if (victoryManager != null)
        {
            Debug.Log("Victory achieved!");
            victoryManager.ShowVictoryScreen();
        }
        else
        {
            Debug.LogError("VictoryManager is null. Check if it's assigned in the Inspector.");
        }
    }
}

