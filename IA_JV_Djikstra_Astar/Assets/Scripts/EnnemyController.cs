using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private GridManager gridManager;  // Reference to the GridManager
    [SerializeField] private float moveSpeed = 2f;     // Speed of movement
    [SerializeField] private Transform playerTransform; // Reference to the player's Transform

    private AStarPathfinding pathfinding; // Reference to the A* pathfinding class
    private List<Vector2> pathToFollow;  // List of tile positions to follow

    // for shooting
    public int health = 50;

    void Start()
    {
        StartCoroutine(WaitForGridGeneration()); // Start the coroutine to wait for the grid to be generated
    }

    // Coroutine to wait until the grid is generated
    IEnumerator WaitForGridGeneration()
    {
        while (!gridManager.isGridGenerated) // Wait until the grid is generated
        {
            yield return null; // Wait for the next frame
        }

        // Initialize A* pathfinding
        pathfinding = new AStarPathfinding(gridManager.GetTiles()); // Initialize the A* pathfinding class with the grid tiles

        // Start continuously following the player
        StartCoroutine(FollowPlayer()); // Start the coroutine to follow the player
    }

    // Coroutine to continuously update the path and follow the player
    IEnumerator FollowPlayer()
    {
        while (true) // Continuously follow the player (probably not the best way to do this, but it works for now)
        {
            // Recalculate the path to the player's current position
            Vector2 start = new Vector2(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y)); // Round to the nearest integer to get the tile position
            Vector2 target = new Vector2(Mathf.RoundToInt(playerTransform.position.x), Mathf.RoundToInt(playerTransform.position.y)); // Round to the nearest integer to get the tile position

            pathToFollow = pathfinding.FindPath(start, target); // Find the path from the enemy to the player

            // If a valid path is found, move along it
            if (pathToFollow != null)
            {
                foreach (Vector2 tilePosition in pathToFollow)
                {
                    Vector3 targetPosition = new Vector3(tilePosition.x, tilePosition.y, 0);
                    yield return StartCoroutine(MoveToPosition(targetPosition));

                    // Update the path to ensure the enemy stays on track if the player moves
                    start = new Vector2(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));
                    target = new Vector2(Mathf.RoundToInt(playerTransform.position.x), Mathf.RoundToInt(playerTransform.position.y));

                    pathToFollow = pathfinding.FindPath(start, target);
                }
            }

            // Wait for a short time before recalculating the path
            yield return new WaitForSeconds(0.5f);  // Wait for 0.5 seconds before recalculating the path we can change it
        }
    }

    // Coroutine to move the enemy to the target position
    IEnumerator MoveToPosition(Vector3 targetPosition)
    {
        Vector3 startPosition = transform.position; // Get the starting position
        float time = 0;

        while (time < 1f) // Move towards the target position
        {
            time += Time.deltaTime * moveSpeed; // Increment the time based on the move speed
            transform.position = Vector3.Lerp(startPosition, targetPosition, time); // Move towards the target position
            yield return null;  // Wait for the next frame
        }

        transform.position = targetPosition; // Ensure final position is correct
    }


    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Handle the enemy's death (e.g., play animation, destroy object)
        Destroy(gameObject);
    }
}
