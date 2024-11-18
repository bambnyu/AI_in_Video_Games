using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;
using UnityEngine.SceneManagement;


public class WinManager : MonoBehaviour
{
    public GameObject WinMenuUI;

    public SheepCount sheepCount;

    public int totalSheep;
    private int sheepInPen;

    private void Start()
    {
        WinMenuUI.SetActive(false); 
    }

    void Update()
    {
        
        if (sheepCount.sheepInPen == totalSheep)
        {
            Win();
        }
    }

    public void Win()
    {
        WinMenuUI.SetActive(true); 
        Time.timeScale = 0f; // Pause game time by setting time scale to 0
    }

    public void NextLevel()
    {
        Debug.Log("Next Level");
        Time.timeScale = 1f;


         
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);  // Load the next scene
    }

    public void QuitGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("StartScene");
    }

}