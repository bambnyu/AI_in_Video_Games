using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Tile : MonoBehaviour
{
    [SerializeField] protected SpriteRenderer renderer;
    [SerializeField] private GameObject highlight;
    public bool typeTest = false;

    public virtual void Init(int x , int y)
    {
        
    }

    void OnMouseEnter()
    {
        highlight.SetActive(true);
    }

    void OnMouseExit()
    {
        highlight.SetActive(false);
    }
}