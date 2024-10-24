using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Shooting properties
    public GameObject bulletPrefab; // Reference to the bullet prefab
    public float fireRate = 1f; // Bullets per second
    private float nextFireTime = 0f;

    // Movement properties
    public float movementSpeed = 2.0f; // Speed of the player movement
    private List<Vector2> currentPath = new List<Vector2>(); // Current path to follow
    private int pathIndex = 0; // Index of the current step in the path
    private bool hasTarget = false; // Whether a target is set for movement

    // Dijkstra pathfinding reference
    private DijkstraPathfinding pathfinding; // Reference to the Dijkstra pathfinding class
    private GridManager gridManager; // Reference to the GridManager

    // Rigidbody for physics-based movement
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Get the rigidbody of the player

        // Find the GridManager if not set
        if (gridManager == null)
        {
            gridManager = FindObjectOfType<GridManager>();
        }

        // Start the coroutine to wait for the grid to be generated and initialize pathfinding
        StartCoroutine(WaitForGridGeneration());
    }

    // Coroutine to wait until the grid is generated
    IEnumerator WaitForGridGeneration()
    {
        while (!gridManager.isGridGenerated) // Wait until the grid is generated
        {
            yield return null; // Wait for the next frame
        }

        // Initialize Dijkstra pathfinding
        pathfinding = new DijkstraPathfinding(gridManager.GetTiles()); // Initialize the Dijkstra pathfinding class with the grid tiles
    }

    void Update()
    {
        // Handle player shooting
        HandleShooting();

        // Handle player movement
        HandleMovement();

        // Handle click input for setting the target tile
        HandleClickInput();
    }

    private void HandleShooting()
    {
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
    }

    private void HandleMovement()
    {
        // Move along the path if there is a valid target and path
        if (hasTarget && currentPath.Count > 0)
        {
            MoveAlongPath();
        }
        else
        {
            // Stop movement if there is no target
            rb.velocity = Vector2.zero;
        }
    }

    private void HandleClickInput()
    {
        // Check if the left mouse button was clicked
        if (Input.GetMouseButtonDown(0))
        {
            // Cast a ray from the camera to the clicked position
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            // Check if the raycast hit a tile
            if (hit.collider != null)
            {
                Tile clickedTile = hit.collider.GetComponent<Tile>();
                if (clickedTile != null && clickedTile.IsWalkable)
                {
                    // Set the new target position
                    Vector2 targetPosition = new Vector2(clickedTile.transform.position.x, clickedTile.transform.position.y);
                    hasTarget = true;
                    currentPath = pathfinding.FindPath(transform.position, targetPosition);
                    pathIndex = 0; // Reset path index
                }
            }
        }
    }

    private void MoveAlongPath()
    {
        if (pathIndex >= currentPath.Count)
        {
            hasTarget = false; // Reached the end of the path
            return;
        }

        // Move towards the next position in the path
        Vector2 nextPosition = currentPath[pathIndex];
        Vector2 direction = (nextPosition - (Vector2)transform.position).normalized;
        rb.velocity = direction * movementSpeed;

        // Check if reached the next position
        if (Vector2.Distance(transform.position, nextPosition) < 0.1f)
        {
            pathIndex++; // Move to the next position in the path
        }
    }

    private void Shoot(GameObject target)
    {
        // Instantiate the bullet at the player's position
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        // Calculate the direction to the target
        Vector3 direction = (target.transform.position - transform.position).normalized;
        // Set the bullet's rotation
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
