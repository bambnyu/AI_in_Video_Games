using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private GridManager gridManager;  // Reference to the GridManager
    [SerializeField] private float moveSpeed = 2f;     // Speed of movement
    [SerializeField] private Transform playerTransform; // Reference to the player's Transform

    private AStarPathfinding pathfinding;
    private List<Vector2> pathToFollow;

    void Start()
    {
        StartCoroutine(WaitForGridGeneration());
    }

    // Coroutine to wait until the grid is generated
    IEnumerator WaitForGridGeneration()
    {
        while (!gridManager.isGridGenerated)
        {
            yield return null;
        }

        // Initialize A* pathfinding
        pathfinding = new AStarPathfinding(gridManager.GetTiles());

        // Define start position as the enemy's current position
        Vector2 start = new Vector2(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));

        // Get the player's current position as the target
        Vector2 target = new Vector2(Mathf.RoundToInt(playerTransform.position.x), Mathf.RoundToInt(playerTransform.position.y));

        // Find the path using A* algorithm
        pathToFollow = pathfinding.FindPath(start, target);

        // Start moving along the path
        if (pathToFollow != null)
        {
            StartCoroutine(MoveAlongPath());
        }
    }

    // Coroutine to move the enemy along the A* path
    IEnumerator MoveAlongPath()
    {
        foreach (Vector2 tilePosition in pathToFollow)
        {
            Vector3 targetPosition = new Vector3(tilePosition.x, tilePosition.y, 0);
            yield return StartCoroutine(MoveToPosition(targetPosition));
        }
    }

    // Coroutine to move the enemy to the target position
    IEnumerator MoveToPosition(Vector3 targetPosition)
    {
        Vector3 startPosition = transform.position;
        float time = 0;

        while (time < 1f)
        {
            time += Time.deltaTime * moveSpeed;
            transform.position = Vector3.Lerp(startPosition, targetPosition, time);
            yield return null;
        }

        transform.position = targetPosition; // Ensure final position is correct
    }
}
