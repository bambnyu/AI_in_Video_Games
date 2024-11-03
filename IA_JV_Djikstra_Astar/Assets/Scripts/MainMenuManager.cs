using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void StartGame()
    {
        // Charge la première scène (assurez-vous qu’elle est au premier index dans les Build Settings)
        SceneManager.LoadScene(1);
    }
}
