using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : Tile
{
    // a wall tile is not walkable
    public override bool IsWalkable => false;
}
