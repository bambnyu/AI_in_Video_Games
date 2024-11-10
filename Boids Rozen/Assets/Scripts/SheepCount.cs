using UnityEngine;
using TMPro;

public class SheepCount : MonoBehaviour
{
    public TMP_Text sheepCounterText; // Reference to the TMP_Text component in the canvas
    private int totalSheep;
    public int sheepInPen { get; private set; }

    void Start()
    {
        // Get total sheep count from BoidManager
        BoidManager boidManager = FindObjectOfType<BoidManager>();
        if (boidManager != null)
        {
            totalSheep = boidManager.flockSize;
        }
        UpdateSheepCounter();
    }

    public void UpdateSheepInPen(int count)
    {
        sheepInPen = count;
        UpdateSheepCounter();
    }

    private void UpdateSheepCounter()
    {
        sheepCounterText.text = $"Sheeps home: {sheepInPen}/{totalSheep}";
    }
}
