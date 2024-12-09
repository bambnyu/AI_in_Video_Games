using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 10; // Damage dealt by the bullet

    

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the bullet hits an object tagged as "Enemy" (like the boss)
        if (collision.CompareTag("Enemy"))
        {
            // Get the BossController or any relevant health script
            BossController boss = collision.GetComponent<BossController>();
            if (boss != null)
            {
                boss.TakeDamage(damage); // Apply damage to the boss
                Debug.Log("damage taken");
            }

            Destroy(gameObject); // Destroy the bullet after collision
        }
    }
}
