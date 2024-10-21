using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public float lifeTime = 6f;
    public int damage = 10;

    public float darkenAmount = 0.15f; // Amount to darken the color on each hit

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

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Bullet collided with: {other.gameObject.name}");

        // Check if the bullet hit an enemy
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Bullet hit an enemy!");

            //// Optionally, change the enemy's color to indicate a hit
            //other.GetComponent<Renderer>().material.color = Color.green;
            DarkenColor(other);


            // // You could call a method on the enemy to apply damage
            other.GetComponent<EnemyController>()?.TakeDamage(damage);
            // Destroy the bullet
            Destroy(gameObject);
        }
    }
    private void DarkenColor(Collider enemy)
    {
        Renderer enemyRenderer = enemy.GetComponent<Renderer>();
        if (enemyRenderer != null)
        {
            // Get the current color
            Color currentColor = enemyRenderer.material.color;

            // Darken the color by reducing RGB values
            float newRed = Mathf.Max(currentColor.r - darkenAmount, 0);
            float newGreen = Mathf.Max(currentColor.g - darkenAmount, 0);
            float newBlue = Mathf.Max(currentColor.b - darkenAmount, 0);

            // Set the new darker color
            enemyRenderer.material.color = new Color(newRed, newGreen, newBlue);
        }
    }
}