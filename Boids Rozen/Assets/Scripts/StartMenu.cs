using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Level1"); // changer le nom des scènes quand ce sera mis au point
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
