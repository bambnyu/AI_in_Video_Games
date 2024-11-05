using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int width, height; // Grid dimensions
    [SerializeField] private Tile grassTile, wallTile, waterTile; // Tile prefabs
    [SerializeField] private Transform cam; // Camera reference
    [SerializeField] private Transform player; // Player reference

    private Dictionary<Vector2, Tile> tiles; // Dictionary to store tiles in the grid
    public bool isGridGenerated { get; private set; } // Flag to indicate grid generation status

    public float probawall = 0.2f;
    public float probawater = 0.07f;

    void Start()
    {
        GenerateGrid(); // Generate the grid
    }

    void GenerateGrid()
    {
        tiles = new Dictionary<Vector2, Tile>(); // Initialize the dictionary
        float wallProbability = probawall; // chance for a tile to be a wall
        float waterProbability = probawater; // chance for a tile to be water

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                // Determine whether to spawn a grass, wall, or water tile
                Tile tileToSpawn;
                float randomValue = Random.value;

                if (randomValue < wallProbability && (x+y!=0)) // Don't put a wall on the spawning tile of the player
                {
                    tileToSpawn = wallTile;
                }
                else if (randomValue < wallProbability + waterProbability)
                {
                    tileToSpawn = waterTile;
                }
                else
                {
                    tileToSpawn = grassTile;
                }

                var spawnedTile = Instantiate(tileToSpawn, new Vector3(x, y), Quaternion.identity); // Spawn the tile
                spawnedTile.name = $"Tile {x} {y}"; // Set the name of the tile
                spawnedTile.Init(x, y); // Initialize the tile
                tiles[new Vector2(x, y)] = spawnedTile; // Add the tile to the dictionary
            }
        }

        // Set the camera and player positions could be changed to be optimized later for different levels and restart options
        cam.transform.position = new Vector3((float)width / 2 - 0.5f, (float)height / 2 - 0.5f, -10); // Set the camera position
        player.position = new Vector3(0, 0, -1); // Set the player position
        isGridGenerated = true; // Set the flag to true after grid generation is done
    }

    public Dictionary<Vector2, Tile> GetTiles() // Getter for the tiles dictionary
    {
        return tiles; // Return the tiles dictionary
    }

    public Vector3 GetRandomGridPosition()
    {
        int randomX = Random.Range(0, width);
        int randomY = Random.Range(0, height);
        return GetWorldPositionFromGrid(randomX, randomY); // Convert grid coordinates to world coordinates
    }

    private Vector3 GetWorldPositionFromGrid(int x, int y)
    {
        return new Vector3(x, y, 0); // Convert grid coordinates to world coordinates could be deleted and directly used in the previous method
    }

    // retrieve a tile at a specific position
    public Tile GetTileAtPosition(Vector2 position)
    {
        tiles.TryGetValue(position, out Tile tile);
        return tile;
    }

    //check if a given position is a water tile
    public bool IsPositionOnWaterTile(Vector2 position)
    {
        // Check if the position has a tile and if it’s a water tile
        if (tiles.TryGetValue(position, out Tile tile))
        {
            return tile is WaterTile; // Return true if the tile is of type WaterTile
        }
        return false; // Return false if no tile or not a WaterTile
    }
    public bool IsPositionOnWallTile(Vector2 position) // not use for now but hey could be used
    {
        if (tiles.TryGetValue(position, out Tile tile))
        {
            return tile == wallTile;
        }
        return false;
    }
}
