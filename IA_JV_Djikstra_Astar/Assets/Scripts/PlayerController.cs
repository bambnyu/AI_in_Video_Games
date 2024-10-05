using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float Speed;
    float speedX, speedY;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        speedX = Input.GetAxis("Horizontal") * Speed;
        speedY = Input.GetAxis("Vertical") * Speed;
        rb.velocity = new Vector2(speedX, speedY);
    }
}
