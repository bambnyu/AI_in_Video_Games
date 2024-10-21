using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Tile : MonoBehaviour
{
    [SerializeField] protected SpriteRenderer renderer; // The sprite renderer of the tile
    //[SerializeField] private GameObject highlight; // The highlight object of the tile (to show when the mouse is over the tile)
    public bool typeTest = false; //is it useful?

    [SerializeField] private GameObject flagPrefab; // Reference to the flag prefab
    // Store the instantiated flag instance
    private GameObject flagInstance;
    private static Tile selectedTile; // Track the currently selected tile

    // Coordinates of the tile
    protected int tileX, tileY;

    public virtual void Init(int x, int y) // Initialize the tile virtual function
    {
        tileX = x; // Set the x coordinate of the tile
        tileY = y; // Set the y coordinate of the tile
    }

    void OnMouseDown()
    {
        // If this tile is already selected, deselect it
        if (selectedTile == this)
        {
            DeselectTile();
        }
        else
        {
            // If there is a previously selected tile, deselect it
            if (selectedTile != null)
            {
                selectedTile.DeselectTile();
            }

            // Select this tile
            SelectTile();
        }

        // Display the coordinates in the console (for debugging)
        Debug.Log($"Tile clicked at coordinates: ({tileX}, {tileY})");
    }

    private void SelectTile()
    {
        // Instantiate the flag prefab at the tile's position
        if (flagPrefab != null)
        {
            flagInstance = Instantiate(flagPrefab, transform.position, Quaternion.identity);
        }
        selectedTile = this; // Update the selected tile reference
    }

    // Method to deselect the tile
    private void DeselectTile()
    {
        // Destroy the flag instance if it exists
        if (flagInstance != null)
        {
            Destroy(flagInstance);
            flagInstance = null;
        }
        selectedTile = null; // Clear the selected tile reference
    }
}
