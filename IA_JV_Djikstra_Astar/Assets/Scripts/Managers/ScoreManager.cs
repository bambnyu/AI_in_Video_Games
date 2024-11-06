using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance; //  create an instance of the ScoreManager class to allow other scripts to access it

    public TextMeshProUGUI scoreText; 
    private int score = 0; // Initial score
    public int scoreToNextLevel = 5; // The score required to trigger the victory screen

    
    [SerializeField] private VictoryManager victoryManager;

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

