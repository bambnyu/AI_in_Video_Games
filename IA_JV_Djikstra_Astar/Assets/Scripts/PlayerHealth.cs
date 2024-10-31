using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;

    public delegate void OnHealthChanged(int currentHealth);
    public static event OnHealthChanged onHealthChanged; // Event for health change

    void Start()
    {
        currentHealth = maxHealth;
        onHealthChanged?.Invoke(currentHealth); // Initialize UI with max health
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Ensure health doesn’t go below 0
        Debug.Log("Player took damage. Current health: " + currentHealth);

        onHealthChanged?.Invoke(currentHealth); // Notify UI of health change

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Player has died.");
        Destroy(gameObject); // Temporary extreme action
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
