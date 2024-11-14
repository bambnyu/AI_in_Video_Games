using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenuUI;

    private bool isPaused = false; // Tracks whether the game is currently paused


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Toggle between pausing and resuming the game
            if (isPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false); // Hide the pause menu UI
        Time.timeScale = 1f; // Resume game time by setting time scale to 1
        isPaused = false; // Update pause state

    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true); // Show the pause menu UI
        Time.timeScale = 0f; // Pause game time by setting time scale to 0
        isPaused = true; // Update pause state

    }

    public void RestartLevel()
    {
        Time.timeScale = 1f; // Ensure time scale is normal before restarting
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reload the current scene
    }

    public void QuitGame()
    {
        Time.timeScale = 1f; // Resume game time by setting time scale to 1
        SceneManager.LoadScene("StartScene");

        //Application.Quit();
    }

}