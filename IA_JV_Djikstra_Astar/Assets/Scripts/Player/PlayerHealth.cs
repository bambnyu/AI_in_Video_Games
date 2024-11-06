using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;

    public float invincibilityDuration = 1.5f; // Time (in seconds) for invincibility
    private bool isInvincible = false; // Tracks if player is currently invincible
    private float invincibilityTimer = 0f; // Timer for invincibility

    public delegate void OnHealthChanged(int currentHealth); // Delegate for health change  dark magic for me but hey internet said it's good and it works soooo
    public static event OnHealthChanged onHealthChanged; // Event for health change

    void Start()
    {
        currentHealth = maxHealth;
        onHealthChanged?.Invoke(currentHealth); // Initialize UI with max health
    }

    void Update()
    {
        if (isInvincible)
        {
            invincibilityTimer -= Time.deltaTime;
            if (invincibilityTimer <= 0)
            {
                isInvincible = false; // End invincibility when timer runs out
            }
        }
    }

    public void TakeDamage(int damage)
    {
        if (isInvincible) return; // Ignore damage if currently invincible

        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Ensure health doesn’t go below 0
        Debug.Log("Player took damage. Current health: " + currentHealth);

        onHealthChanged?.Invoke(currentHealth); // Notify UI of health change

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            // Start invincibility period
            isInvincible = true;
            invincibilityTimer = invincibilityDuration;
        }
    }

    private void Die()
    {
        FindObjectOfType<GameOverManager>().ShowGameOver();
        //Debug.Log("Player has died.");
        //Destroy(gameObject); // Temporary extreme action for test
        // but should be replaced with a game over screen or something
        // a restart button or something 
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            TakeDamage(1); // Each enemy hit deals 1 damage
        }
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }
}
