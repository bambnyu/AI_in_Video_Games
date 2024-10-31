using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public float lifeTime = 6f;
    public int damage = 10;
    public float darkenAmount = 0.2f; // Amount to darken the color on each hit

    private void Start()
    {
        // Destroy the bullet after a certain time to prevent memory leaks
        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        // Move the bullet forward
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"Bullet collided with: {other.gameObject.name}");

        // Check if the bullet hit an enemy
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Bullet hit an enemy!");

            // Apply color darkening
            DarkenColor(other);

            // Attempt to apply damage to the enemy
            var enemyController = other.GetComponent<EnemyController>();
            var dijkstraEnemyController = other.GetComponent<DijkstraEnemyController>();

            if (enemyController != null)
            {
                enemyController.TakeDamage(damage);
            }
            else if (dijkstraEnemyController != null)
            {
                dijkstraEnemyController.TakeDamage(damage);
            }

            // Destroy the bullet after hitting the enemy
            Destroy(gameObject);
        }
    }

    private void DarkenColor(Collider2D enemy)
    {
        Renderer enemyRenderer = enemy.GetComponent<Renderer>();
        if (enemyRenderer != null && enemyRenderer.material != null)
        {
            // Get the current color and darken by reducing RGB values
            Color currentColor = enemyRenderer.material.color;
            Color newColor = new Color(
                Mathf.Max(currentColor.r - darkenAmount, 0),
                Mathf.Max(currentColor.g - darkenAmount, 0),
                Mathf.Max(currentColor.b - darkenAmount, 0)
            );

            // Set the new darker color
            enemyRenderer.material.color = newColor;
        }
    }
}
