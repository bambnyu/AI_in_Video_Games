using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class LifeUI : MonoBehaviour
{
    public List<Image> heartImages = new List<Image>(); // List of heart images

    void OnEnable()
    {
        PlayerHealth.onHealthChanged += UpdateHearts; // Subscribe to the event
    }

    void OnDisable()
    {
        PlayerHealth.onHealthChanged -= UpdateHearts; // Unsubscribe from the event
    }

    private void UpdateHearts(int currentHealth)
    {
        for (int i = 0; i < heartImages.Count; i++)
        {
            heartImages[i].enabled = i < currentHealth; // Enable hearts up to the current health level
        }
    }
}
