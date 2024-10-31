using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Player took damage. Current health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Player has died.");
        // here destroy character 
        Destroy(gameObject); // extreme mais temporaire
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the player collided with an enemy
        if (other.CompareTag("Enemy"))
        {
            TakeDamage(1); // Assuming each enemy hit deals 1 damage
        }
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }
}
