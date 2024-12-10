using UnityEngine;

public class BossBullet : MonoBehaviour
{
    public int damage = 10; // Damage dealt by the boss's bullet
    public Animator animator; // Animator for bullet animations


    private string currentPhaseAnimation;

    // Set the animation state based on the boss's phase
    public void SetPhaseAnimation(string phase)
    {
        if (animator != null)
        {
            // Reset all boolean parameters to false
            animator.SetBool("Phase1", false);
            animator.SetBool("Phase2", false);
            animator.SetBool("Enraged", false);

            // Activate the correct phase boolean
            switch (phase)
            {
                case "Phase1Bullet":
                    animator.SetBool("Phase1", true);
                    break;
                case "Phase2Bullet":
                    animator.SetBool("Phase2", true);
                    break;
                case "EnragedBullet":
                    animator.SetBool("Enraged", true);
                    break;
            }
        }
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the bullet hits the player
        if (collision.CompareTag("Player"))
        {
            // Get the PlayerController script or any relevant health script
            PlayerController player = collision.GetComponent<PlayerController>();
            if (player != null)
            {
                player.TakeDamage(damage); // Apply damage to the player
                Debug.Log("Player hit by boss bullet!");
            }

            Destroy(gameObject); // Destroy the bullet after collision
        }
    }
}
