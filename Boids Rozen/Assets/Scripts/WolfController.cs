using UnityEngine;

public class WolfController : MonoBehaviour
{
    public float speed = 5f;
    public Vector2 boxSize = new Vector2(39, 17);

    void Update()
    {
        // Get input for movement
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Calculate movement
        Vector2 movement = new Vector2(moveHorizontal, moveVertical).normalized * speed * Time.deltaTime;
        transform.Translate(movement);

        // Clamp position within the box boundaries
        float clampedX = Mathf.Clamp(transform.position.x, -boxSize.x / 2, boxSize.x / 2);
        float clampedY = Mathf.Clamp(transform.position.y, -boxSize.y / 2, boxSize.y / 2);
        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }
}
