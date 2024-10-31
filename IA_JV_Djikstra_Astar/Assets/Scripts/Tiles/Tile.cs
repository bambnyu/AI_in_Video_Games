using System.Collections;
using UnityEngine;

public abstract class Tile : MonoBehaviour
{
    [SerializeField] protected SpriteRenderer renderer; // The sprite renderer of the tile
    [SerializeField] private GameObject flagPrefab; // Reference to the flag prefab
    private GameObject flagInstance; // Store the instantiated flag instance

    public static Tile selectedTile; // Track the currently selected tile
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

    private void OnMouseDown()
    {
        // Toggle selection state
        if (selectedTile == this)
        {
            DeselectTile();
        }
        else
        {
            selectedTile?.DeselectTile();
            SelectTile();
        }

        Debug.Log($"Tile clicked at coordinates: ({tileX}, {tileY})");
    }

    public void SelectTile()
    {
        // Instantiate flag at the tile's position
        if (flagPrefab != null)
        {
            flagInstance = Instantiate(flagPrefab, transform.position, Quaternion.identity);
        }
        selectedTile = this;
    }

    public void DeselectTile()
    {
        // Destroy the flag if it exists
        if (flagInstance != null)
        {
            Destroy(flagInstance);
            flagInstance = null;
        }
        selectedTile = null;
    }
}
