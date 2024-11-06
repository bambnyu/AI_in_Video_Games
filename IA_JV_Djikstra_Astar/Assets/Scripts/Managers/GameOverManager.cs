using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverUI;

    public void ShowGameOver()
    {
        gameOverUI.SetActive(true); // Display the Game Over UI
        Time.timeScale = 0f; // Pause the game by setting time scale to 0
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f; // Reset the time scale to normal speed
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reload the current scene
    }
}
