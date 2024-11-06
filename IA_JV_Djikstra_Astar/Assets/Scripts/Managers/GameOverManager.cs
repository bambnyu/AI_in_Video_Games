using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverUI;
    public AudioClip gameOverSound; // Sound for Game Over
    private AudioSource audioSource;

    void Start()
    {
        // Add or get an AudioSource component
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false; // Don't play on awake
    }

    public void ShowGameOver()
    {
        gameOverUI.SetActive(true); // Display the Game Over UI
        Time.timeScale = 0f; // Pause the game by setting time scale to 0
        if (gameOverSound != null)
        {
            audioSource.PlayOneShot(gameOverSound); // Play the game over sound
        }
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f; // Reset the time scale to normal speed
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reload the current scene
    }
}
