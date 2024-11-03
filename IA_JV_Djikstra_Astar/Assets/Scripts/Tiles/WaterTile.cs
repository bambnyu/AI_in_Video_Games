using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTile : Tile
{
    // a water tile is walkable but has a higher crossing cost
    public WaterTile()
    {
        IsWater = true; // Identify this tile as a water tile
        CrossingCost = 4f; // Set a higher crossing cost for water tiles
    }
}
