using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public void StartGame()
    {
        Time.timeScale = 1f; // to be sure the time scale is normal
        SceneManager.LoadScene("Level"); 
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}