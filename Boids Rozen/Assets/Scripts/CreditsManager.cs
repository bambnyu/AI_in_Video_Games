using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsManager : MonoBehaviour
{
    public void BackToStart()
    {
        Time.timeScale = 1f; // to be sure the time scale is normal
        SceneManager.LoadScene("StartScene"); 
    }
}
