using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance; //  create an instance of the ScoreManager class to allow other scripts to access it

    public TextMeshProUGUI scoreText; // Reference to the TextMeshPro score text 
    private int score = 0; // Initial score

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
    }

    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + score; // Update the TextMeshPro UI text
    }
}
