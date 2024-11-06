using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public AudioClip startGameSound; // Sound to play when starting the game
    private AudioSource audioSource;

    void Start()
    {
        // Add or get an AudioSource component
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    public void StartGame()
    {
        // Play the start game sound
        if (startGameSound != null)
        {
            audioSource.PlayOneShot(startGameSound);
            // Delay loading the scene until the sound finishes
            Invoke("LoadLevel1", startGameSound.length);
        }
        else
        {
            LoadLevel1(); // Load immediately if no sound is set
        }
    }

    private void LoadLevel1()
    {
        // Load the first scene (level1)
        SceneManager.LoadScene(1);
    }
}
