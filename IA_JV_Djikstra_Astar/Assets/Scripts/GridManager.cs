using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int width, height; // Grid dimensions
    [SerializeField] private Tile grassTile, wallTile; // Tile prefabs
    [SerializeField] private Transform cam; // test for camera
    [SerializeField] private Transform player; // test for player

    private Dictionary<Vector2, Tile> tiles; // Dictionary to store tiles in the grid
    public bool isGridGenerated { get; private set; } // Flag to indicate grid generation status

    void Start()
    {
        GenerateGrid(); // Generate the grid
    }

    void GenerateGrid()
    {
        tiles = new Dictionary<Vector2, Tile>(); // Initialize the dictionary
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var spawnedTile = Instantiate(grassTile, new Vector3(x, y), Quaternion.identity); // Spawn a tile
                spawnedTile.name = $"Tile {x} {y}"; // Set the name of the tile
                spawnedTile.Init(x, y); // Initialize the tile
                tiles[new Vector2(x, y)] = spawnedTile; // Add the tile to the dictionary
            }
        }

        cam.transform.position = new Vector3((float)width / 2 - 0.5f, (float)height / 2 - 0.5f, -10); // Set the camera position
        player.position = new Vector3(0, 0, -1); // Set the player position
        isGridGenerated = true;  // Set the flag to true after grid generation is done because we had problems with the priority for the rest of the code
    }

    public Dictionary<Vector2, Tile> GetTiles() // Getter for the tiles dictionary
    {
        return tiles; // Return the tiles dictionary
    }


    public Vector3 GetRandomGridPosition()
    {
        // Assuming your grid is defined with a certain width and height
        int randomX = Random.Range(0, width);
        int randomY = Random.Range(0, height);

        // Convert the grid coordinates to world coordinates
        Vector3 randomPosition = GetWorldPositionFromGrid(randomX, randomY);

        return randomPosition;
    }

    private Vector3 GetWorldPositionFromGrid(int x, int y)
    {
        // Assuming each tile is a unit square, adjust based on your grid configuration
        return new Vector3(x, y, 0); // inverser y 0
    }
}