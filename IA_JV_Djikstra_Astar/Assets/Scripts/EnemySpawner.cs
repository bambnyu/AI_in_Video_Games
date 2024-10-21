using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Reference to the enemy prefab
    public float spawnInterval = 3f; // Time interval in seconds between spawns
    public GridManager gridManager; // Reference to your grid manager

    private void Start()
    {
        // Start spawning enemies at regular intervals
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            SpawnEnemyAtRandomPosition();
            // Wait for the specified interval before spawning the next enemy
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnEnemyAtRandomPosition()
    {
        // Get a random grid position
        Vector3 randomPosition = gridManager.GetRandomGridPosition();

        // Instantiate the enemy at the random position
        Instantiate(enemyPrefab, randomPosition, Quaternion.identity);
    }
}