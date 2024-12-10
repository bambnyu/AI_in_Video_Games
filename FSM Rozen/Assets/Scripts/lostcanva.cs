using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class lostcanva : MonoBehaviour
{
    public void RestarttGame()
    {
        Time.timeScale = 1f; // to be sure the time scale is normal
        SceneManager.LoadScene("Level");
    }

    public void AbandonGame()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
