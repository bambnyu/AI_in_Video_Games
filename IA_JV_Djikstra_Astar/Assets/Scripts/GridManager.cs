using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int width, height;
    [SerializeField] private Tile grassTile, wallTile;
    [SerializeField] private Transform cam;
    [SerializeField] private Transform player; // test pour poser le joueur

    private Dictionary<Vector2, Tile> tiles;
    public bool isGridGenerated { get; private set; } // Flag to indicate grid generation status

    void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        tiles = new Dictionary<Vector2, Tile>();
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var spawnedTile = Instantiate(grassTile, new Vector3(x, y), Quaternion.identity);
                spawnedTile.name = $"Tile {x} {y}";
                spawnedTile.Init(x, y);
                tiles[new Vector2(x, y)] = spawnedTile;
            }
        }

        cam.transform.position = new Vector3((float)width / 2 - 0.5f, (float)height / 2 - 0.5f, -10);
        player.position = new Vector3(0, 0, -1);
        isGridGenerated = true;  // Set the flag to true after grid generation is done
    }

    public Dictionary<Vector2, Tile> GetTiles()
    {
        return tiles;
    }
}