using UnityEngine;
using TMPro; 
using UnityEngine.SceneManagement;

public class LevelTimer : MonoBehaviour
{
    public float levelTime = 120f; // Set in game depending on level
    private float timeRemaining;
    public TextMeshProUGUI timerText; 
    public GameObject lostCanvas; 

    void Start()
    {
        Time.timeScale = 1f; // Ensure time scale is normal a bad fix for the load bug
        timeRemaining = levelTime;
        lostCanvas.SetActive(false); 
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
        SceneManager.LoadScene("StartScene"); // Load the start scene
    }
}
