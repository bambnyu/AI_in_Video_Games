using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassTile : Tile
{
    [SerializeField] private Color baseColor, offsetColor;

    public override bool IsWalkable => true;

    public override void Init(int x, int y)
    {
        base.Init(x, y);  // Call the base method to set coordinates
        var isOffset = (x + y) % 2 == 1; // Check if the tile is the offset one
        renderer.color = isOffset ? offsetColor : baseColor; // Set the color of the tile to crete the checkerboard pattern
    }

}
