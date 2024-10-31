using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DijkstraEnemyController : MonoBehaviour
{
    [SerializeField] private GridManager gridManager;  // Reference to the GridManager
    [SerializeField] private float normalMoveSpeed = 2f; // Normal speed of movement
    [SerializeField] private float waterMoveSpeed = 0.5f;  // Reduced speed on water tiles
    [SerializeField] private Transform playerTransform; // Reference to the player's Transform

    private float currentMoveSpeed; // Current speed of the enemy
    private DijkstraPathfinding pathfinding; // Reference to the Dijkstra pathfinding class
    private List<Vector2> pathToFollow;  // List of tile positions to follow

    // For health management
    public int health = 30;

    void Start()
    {
        if (gridManager == null)
        {
            gridManager = FindObjectOfType<GridManager>();
        }
        if (playerTransform == null)
        {
            playerTransform = FindObjectOfType<PlayerController>().transform;
        }
        currentMoveSpeed = normalMoveSpeed; // Set the initial speed
        StartCoroutine(WaitForGridGeneration());
    }

    IEnumerator WaitForGridGeneration()
    {
        while (!gridManager.isGridGenerated)
        {
            yield return null;
        }

        pathfinding = new DijkstraPathfinding(gridManager.GetTiles());
        StartCoroutine(FollowPlayer());
    }

    IEnumerator FollowPlayer()
    {
        while (true)
        {
            Vector2 start = new Vector2(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));
            Vector2 target = new Vector2(Mathf.RoundToInt(playerTransform.position.x), Mathf.RoundToInt(playerTransform.position.y));

            pathToFollow = pathfinding.FindPath(start, target);

            if (pathToFollow != null && pathToFollow.Count > 0)
            {
                foreach (Vector2 tilePosition in pathToFollow)
                {
                    Vector3 targetPosition = new Vector3(tilePosition.x, tilePosition.y, 0);
                    yield return StartCoroutine(MoveToPosition(targetPosition));

                    // Update the path to track the player's movement
                    start = new Vector2(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));
                    target = new Vector2(Mathf.RoundToInt(playerTransform.position.x), Mathf.RoundToInt(playerTransform.position.y));

                    pathToFollow = pathfinding.FindPath(start, target);
                }
            }

            yield return new WaitForSeconds(0.5f);
        }
    }

    IEnumerator MoveToPosition(Vector3 targetPosition)
    {
        Vector3 startPosition = transform.position;
        float time = 0;

        // Adjust speed based on tile type
        AdjustSpeedBasedOnTile(targetPosition);

        while (time < 1f)
        {
            time += Time.deltaTime * currentMoveSpeed;
            transform.position = Vector3.Lerp(startPosition, targetPosition, time);
            yield return null;
        }

        transform.position = targetPosition;
    }

    private void AdjustSpeedBasedOnTile(Vector3 targetPosition)
    {
        Vector2 tilePosition = new Vector2(Mathf.RoundToInt(targetPosition.x), Mathf.RoundToInt(targetPosition.y));

        // Check if the tile at the target position is a water tile
        if (gridManager.IsPositionOnWaterTile(tilePosition))
        {
            currentMoveSpeed = waterMoveSpeed; // Use reduced speed on water tiles
        }
        else
        {
            currentMoveSpeed = normalMoveSpeed; // Use normal speed on other tiles
        }
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
        Destroy(gameObject);
    }
}
