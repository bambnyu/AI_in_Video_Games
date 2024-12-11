using UnityEngine;
using UnityEngine.UI;
using TMPro; // Use this if you're using TextMeshPro

public class LifeCanvas : MonoBehaviour
{
    [Header("UI Settings")]
    public TextMeshProUGUI lifeText; // Use TextMeshProUGUI for TMP, or Text for default UI
    public PlayerController player;

    void Start()
    {
        UpdateLifeText();
    }

    void Update()
    {
        UpdateLifeText();
    }

    void UpdateLifeText()
    {
        lifeText.text = $"Life: {player.currentHealth/10}/{player.maxHealth/10}";
    }
}
