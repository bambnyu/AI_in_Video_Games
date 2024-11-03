using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs; // Array of enemy prefabs we only have two for now Djikstra and AStar
    public float spawnInterval = 3f; // Time interval in seconds between spawns ---> could be changed to a range for more randomness
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
        // i should check if the position is not occupied by another enemy or the player or the walls !!!!
        Vector3 randomPosition = gridManager.GetRandomGridPosition();

        // Select a random enemy prefab from the array
        int randomIndex = Random.Range(0, enemyPrefabs.Length);
        GameObject randomEnemyPrefab = enemyPrefabs[randomIndex];

        // Instantiate the enemy at the random position
        Instantiate(randomEnemyPrefab, randomPosition, Quaternion.identity);
    }
}
