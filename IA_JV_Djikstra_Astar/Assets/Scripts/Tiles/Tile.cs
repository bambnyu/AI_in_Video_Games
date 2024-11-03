using System.Collections;
using UnityEngine;

public abstract class Tile : MonoBehaviour
{
    [SerializeField] protected SpriteRenderer renderer; // The sprite renderer of the tile, not sure if this is the best way to do this but necessary for the grass tile checkerboard pattern
    public int tileX, tileY; // Coordinates of the tile 

    // Properties to set walkability and cost
    public virtual bool IsWalkable { get; protected set; } = true; // Default walkability to true
    public bool IsWater { get; protected set; } = false; // Default to non-water tile
    public float CrossingCost { get; protected set; } = 1f; // Default cost for standard tile

    public virtual void Init(int x, int y) // Initialize the tile
    {
        tileX = x;
        tileY = y;
    }
}
