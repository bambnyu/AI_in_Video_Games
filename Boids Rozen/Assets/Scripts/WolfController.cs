using UnityEngine;

public class WolfController : MonoBehaviour
{
    public float speed = 5f;

    void Update()
    {
        // Get input for movement
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Update position based on input
        Vector2 movement = new Vector2(moveHorizontal, moveVertical).normalized * speed * Time.deltaTime;
        transform.Translate(movement);
    }
}
