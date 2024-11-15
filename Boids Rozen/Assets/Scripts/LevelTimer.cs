using UnityEngine;
using TMPro; // Import TextMeshPro namespace
using UnityEngine.SceneManagement;

public class LevelTimer : MonoBehaviour
{
    public float levelTime = 120f; // Set to 2 minutes (120 seconds)
    private float timeRemaining;
    public TextMeshProUGUI timerText; // Reference to the TMP Text for the timer
    public GameObject lostCanvas; // Reference to the Lost Canvas

    void Start()
    {
        timeRemaining = levelTime;
        lostCanvas.SetActive(false); // Hide the lost canvas at the start
    }

    void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            UpdateTimerDisplay(timeRemaining);
        }
        else
        {
            TimeUp();
        }
    }

    void UpdateTimerDisplay(float timeToDisplay)
    {
        // Format the timer as minutes:seconds
        int minutes = Mathf.FloorToInt(timeToDisplay / 60);
        int seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void TimeUp()
    {
        // Enable the lost canvas when time's up
        lostCanvas.SetActive(true);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reload the current scene
    }

    public void QuitGame()
    {
        SceneManager.LoadScene("StartScene");

        //Application.Quit();
    }
}
