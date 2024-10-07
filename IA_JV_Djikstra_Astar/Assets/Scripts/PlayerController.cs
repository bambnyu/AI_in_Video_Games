using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Refactor it completely because it's not how we want to move the player
// we want the player to move from tile to tile using astar or dijkstra to go to a clicked tile
public class PlayerController : MonoBehaviour
{
    public float Speed; // Speed of the player
    float speedX, speedY; // Speed on X and Y axis
    Rigidbody2D rb; // Rigidbody of the player

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Get the rigidbody of the player 
    }

    // Update is called once per frame
    void Update()
    {
        speedX = Input.GetAxis("Horizontal") * Speed; // Get the speed on X axis
        speedY = Input.GetAxis("Vertical") * Speed; // Get the speed on Y axis
        rb.velocity = new Vector2(speedX, speedY); // Set the velocity of the player
    }
}
