using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void StartGame()
    {
        // load the first scene (level1)
        SceneManager.LoadScene(1);
    }
}
