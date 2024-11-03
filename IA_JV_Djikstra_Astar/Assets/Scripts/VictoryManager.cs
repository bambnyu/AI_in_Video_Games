using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryManager : MonoBehaviour
{
    public GameObject victoryUI; // Reference to the Victory Canvas UI

    public void ShowVictoryScreen()
    {
        victoryUI.SetActive(true); // Show the Victory Screen
        Time.timeScale = 0f; // Pause the game when the victory screen appears
    }

    public void LoadNextLevel()
    {
        Time.timeScale = 1f; // Resume the game time
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // Load the next level
    }
}
