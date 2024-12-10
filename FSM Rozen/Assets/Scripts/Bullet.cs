using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 10;

    

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the bullet hits the enemy
        if (collision.CompareTag("Enemy"))
        {
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
