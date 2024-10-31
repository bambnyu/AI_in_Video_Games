using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTile : Tile
{
    [SerializeField] private Color baseColor, offsetColor;

    // Override walkability and set higher crossing cost for water tiles
    public override bool IsWalkable => true;

    public WaterTile()
    {
        IsWater = true; // Identify this tile as a water tile
        CrossingCost = 4f; // Set a higher crossing cost for water tiles
    }

    public override void Init(int x, int y)
    {
        base.Init(x, y); // Set coordinates
        var isOffset = (x + y) % 2 == 1; // Checkerboard pattern
        renderer.color = isOffset ? offsetColor : baseColor; // Set color based on offset
    }
}
