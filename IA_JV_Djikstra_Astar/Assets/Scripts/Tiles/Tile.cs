using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Tile : MonoBehaviour
{
    [SerializeField] protected SpriteRenderer renderer;
    [SerializeField] private GameObject highlight;
    public bool typeTest = false;

    // Coordinates of the tile
    protected int tileX, tileY;

    public virtual void Init(int x, int y)
    {
        tileX = x;
        tileY = y;
    }

    void OnMouseEnter()
    {
        highlight.SetActive(true);
    }

    void OnMouseExit()
    {
        highlight.SetActive(false);
    }

    void OnMouseDown()
    {
        // Change the color of the tile to dark green
        renderer.color = new Color32(23, 86, 22,255);

        // Display the coordinates in the console
        Debug.Log($"Tile clicked at coordinates: ({tileX}, {tileY})");
    }
}
