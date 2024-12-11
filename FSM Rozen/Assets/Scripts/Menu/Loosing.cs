using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loosing : MonoBehaviour
{
    public GameObject lostCanvas;
    public PlayerController playerController;

    void Start()
    {
        Time.timeScale = 1f; // to be sure the time scale is normal
        lostCanvas.SetActive(false);
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        Debug.Log(playerController);
    }

    public void ShowLostCanvas()
    {
        lostCanvas.SetActive(true);
        Time.timeScale = 0f; 
        
    }



    // Update is called once per frame
    void Update()
    {
        if (playerController.currentHealth <= 0)
        {
            ShowLostCanvas();
        }
    }
}
