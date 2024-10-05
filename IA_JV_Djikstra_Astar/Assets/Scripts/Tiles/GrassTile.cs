using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassTile : Tile
{
    [SerializeField] private Color baseColor, offsetColor;

    public override void Init(int x, int y)
    {
        base.Init(x, y);  // Call the base method to set coordinates
        var isOffset = (x + y) % 2 == 1;
        renderer.color = isOffset ? offsetColor : baseColor;
    }

}
