using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Tile : MonoBehaviour
{
    [SerializeField] protected SpriteRenderer renderer; // The sprite renderer of the tile
    [SerializeField] private GameObject highlight; // The highlight object of the tile (to show when the mouse is over the tile)
    public bool typeTest = false; //is it useful?

    // Coordinates of the tile
    protected int tileX, tileY;

    public virtual void Init(int x, int y) // Initialize the tile virtual function
    {
        tileX = x; // Set the x coordinate of the tile
        tileY = y; // Set the y coordinate of the tile
    }

    void OnMouseDown() // When the mouse clicks on the tile
    {
        // Change the color of the tile to dark green
        renderer.color = new Color32(23, 86, 22, 255); // we will have to change that so when the player click on a tile it becomes dark green and when he click on another tile the previous one becomes the original color thans to a second highlight object

        // Display the coordinates in the console
        Debug.Log($"Tile clicked at coordinates: ({tileX}, {tileY})"); // just some debug to delete once everything works
    }
}
