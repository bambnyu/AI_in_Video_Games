using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // for shooting
    public GameObject bulletPrefab; // Reference to the bullet prefab
    public float fireRate = 1f; // Bullets per second ---> do I make a way to change this in the game???
    private float nextFireTime = 0f; // Time to fire the next bullet

    Rigidbody2D rb; // Rigidbody of the player

    // movement limitations for the player inside the grid not the best i should just put a collider on the grid or use wall tiles to limit the player's movement
    public float limitGauche = 0.0f;
    public float limitDroite = 16.0f;
    public float limitHaut = 10.0f;
    public float limitBas = 0.0f;


    float speedX, speedY; // Speed on X and Y axis
    public float normalSpeed = 2.0f; // Normal speed of the player
    public float waterSpeed = 0.50f; // Reduced speed on water tiles
    private float currentSpeed;

    private GridManager gridManager;
        
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Get the rigidbody of the player 
        gridManager = FindObjectOfType<GridManager>(); // Reference to the GridManager
        currentSpeed = normalSpeed; // Set the initial speed
    }

    // Update is called once per frame
    void Update()
    {
        // Get the player's position 
        Vector2 playerPosition = new Vector2(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));

        // Check if the player is on a water tile
        if (gridManager.IsPositionOnWaterTile(playerPosition))
        {
            currentSpeed = waterSpeed; // Slow down on water
        }
        else
        {
            currentSpeed = normalSpeed; // Use normal speed on other tiles
        }

        // Get the input from the player
        speedX = Input.GetAxis("Horizontal") * currentSpeed; // Get the speed on X axis
        speedY = Input.GetAxis("Vertical") * currentSpeed; // Get the speed on Y axis
        rb.velocity = new Vector2(speedX, speedY); // Set the velocity of the player


        // Check if it's time to fire
        if (Time.time >= nextFireTime)
        {
            // Find the nearest enemy
            GameObject nearestEnemy = FindNearestEnemy();
            if (nearestEnemy != null)
            {
                // Shoot towards the enemy
                Shoot(nearestEnemy);
                nextFireTime = Time.time + 1f / fireRate;
            }
        }

        // Limit the player's movement inside the grid
        Vector3 position = transform.position;

        if (position.x < limitGauche)
        {
            position.x = limitGauche;
        }
        if (position.x > limitDroite)
        {
            position.x = limitDroite;
        }
        if (position.y > limitHaut)
        {
            position.y = limitHaut;
        }
        if (position.y < limitBas)
        {
            position.y = limitBas;
        }

        transform.position = position; // Set the new position of the player
    }

    private void Shoot(GameObject target)
    {
        // Instantiate the bullet at the player's position
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        // Calculate the direction to the target
        Vector3 direction = (target.transform.position - transform.position).normalized;
        // Set the bullet's rotation probably not necessary since the bullet is a sphere but if I want to use a sprite it should be useful
        bullet.transform.rotation = Quaternion.LookRotation(direction);
    }

    private GameObject FindNearestEnemy()
    {
        // Find all enemies in the scene
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject nearestEnemy = null;
        float closestDistance = Mathf.Infinity;

        // Loop through each enemy to find the closest one
        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                nearestEnemy = enemy;
            }
        }

        return nearestEnemy;
    }
}
