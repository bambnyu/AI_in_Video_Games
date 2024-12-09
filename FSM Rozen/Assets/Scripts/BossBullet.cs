using UnityEngine;

public class BossBullet : MonoBehaviour
{
    public int damage = 10; // Damage dealt by the boss's bullet

    
    void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the bullet hits the player
        if (collision.CompareTag("Player"))
        {
            // Get the PlayerController script or any relevant health script
            PlayerController player = collision.GetComponent<PlayerController>();
            if (player != null)
            {
                player.TakeDamage(damage); // Apply damage to the player
                Debug.Log("Player hit by boss bullet!");
            }

            Destroy(gameObject); // Destroy the bullet after collision
        }
    }
}
