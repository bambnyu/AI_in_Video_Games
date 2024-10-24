using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : Tile
{
    public override bool IsWalkable => false;
    void OnMouseDown()
    {
        // Do nothing to prevent interaction with the WallTile
    }
}
