using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DijkstraEnemyController : MonoBehaviour
{
    [SerializeField] private GridManager gridManager;  // Reference to the GridManager
    [SerializeField] private float normalMoveSpeed = 2f; // Normal speed of movement
    [SerializeField] private float waterMoveSpeed = 0.5f;  // Reduced speed on water tiles
    [SerializeField] private Transform playerTransform; // Reference to the player's Transform to calculate the path

    private float currentMoveSpeed; // Current speed of the enemy
    private DijkstraPathfinding pathfinding; // Reference to the Dijkstra pathfinding class
    private List<Vector2> pathToFollow;  // List of tile positions to follow

    public int health = 30; // life points of the enemy

    private Animator animator;
    public AudioClip hitSound; // Sound effect for taking damage
    private AudioSource audioSource; // Reference to the AudioSource component

    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        // Find the GridManager and PlayerController
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
        // Wait for the grid to be generated before starting the pathfinding, i need to do it because the pathfinding class needs the grid to be generated 
        while (!gridManager.isGridGenerated)
        {
            yield return null;
        }

        // Initialize the pathfinding class and start following the player
        pathfinding = new DijkstraPathfinding(gridManager.GetTiles());
        StartCoroutine(FollowPlayer());
    }

    IEnumerator FollowPlayer()
    {
        while (true) // Loop to keep following the player until the enemy dies but it's not the best way to do it 
        {
            // find the start and target positions which are the current position of the enemy and the player's position
            Vector2 start = new Vector2(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));
            Vector2 target = new Vector2(Mathf.RoundToInt(playerTransform.position.x), Mathf.RoundToInt(playerTransform.position.y));

            // Find the path to follow
            pathToFollow = pathfinding.FindPath(start, target);

            if (pathToFollow != null && pathToFollow.Count > 0) // not sure if the second condition is necessary for the count
            {
                // Follow the path to the player
                foreach (Vector2 tilePosition in pathToFollow)
                {
                    // Move to the target position
                    Vector3 targetPosition = new Vector3(tilePosition.x, tilePosition.y, 0);
                    yield return StartCoroutine(MoveToPosition(targetPosition));

                    // Update the path to track the player's movement
                    start = new Vector2(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));
                    target = new Vector2(Mathf.RoundToInt(playerTransform.position.x), Mathf.RoundToInt(playerTransform.position.y));

                    pathToFollow = pathfinding.FindPath(start, target);
                }
            }

            yield return new WaitForSeconds(0.001f); // Wait for a short time before recalculating the path
        }
    }

    IEnumerator MoveToPosition(Vector3 targetPosition)
    {
        Vector3 startPosition = transform.position;
        float time = 0;

        AdjustSpeedBasedOnTile(targetPosition);

        // Determine direction based on the target position relative to the current position
        Vector3 direction = (targetPosition - startPosition).normalized;
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            // Moving horizontally
            animator.SetInteger("Direction", direction.x > 0 ? 3 : 2); // 3: Right, 2: Left
        }
        else
        {
            // Moving vertically
            animator.SetInteger("Direction", direction.y > 0 ? 1 : 0); // 1: Up, 0: Down
        }

        while (time < 1f)
        {
            time += Time.deltaTime * currentMoveSpeed;
            transform.position = Vector3.Lerp(startPosition, targetPosition, time);
            yield return null;
        }

        transform.position = targetPosition; // Ensure the enemy is at the target position
    }



    private void AdjustSpeedBasedOnTile(Vector3 targetPosition)
    {
        // Get the position of the tile
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
        if (hitSound != null)
        {
            audioSource.PlayOneShot(hitSound);
        }
        // Reduce the health of the enemy and check if it is dead
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        animator.SetBool("isDead", true);
        ScoreManager.instance.AddScore(1); // Increase score by 1
        Destroy(gameObject);
        //maybe add some particle effects or sound effects
    }
}
