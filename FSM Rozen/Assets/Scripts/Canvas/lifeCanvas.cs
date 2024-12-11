using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LifeCanvas : MonoBehaviour
{
    [Header("UI Settings")]
    public TextMeshProUGUI lifeText;
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
        lifeText.text = $"Life: {player.currentHealth/10}/{player.maxHealth/10}"; // Update the life text
    }
}
