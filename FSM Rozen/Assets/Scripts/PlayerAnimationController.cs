using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    public enum PlayerState
    {
        Idle,
        Run,
        Jump,
        Dash,
        ShootIdle,
        ShootRunning,
        Damage
    }

    [Header("References")]
    public Animator animator; // Reference to the Animator component
    public PlayerController playerController; // Reference to the PlayerController script

    private PlayerState currentState = PlayerState.Idle;

    void Start()
    {
        if (animator == null)
            animator = GetComponent<Animator>();

        if (playerController == null)
            playerController = GetComponent<PlayerController>();
    }

    void Update()
    {
        UpdateState();
        UpdateAnimator();
    }

    private void UpdateState()
    {
        // Check if the player is dashing
        if (playerController.isDashing)
        {
            SetState(PlayerState.Dash);
        }
        // Check if the player is jumping
        else if (!playerController.isGrounded)
        {
            SetState(PlayerState.Jump);
        }
        // Check if the player is shooting
        else if (Input.GetButton("Fire1"))
        {
            if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1f)
                SetState(PlayerState.ShootRunning);
            else
                SetState(PlayerState.ShootIdle);
        }
        // Check if the player is running
        else if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1f && playerController.isGrounded)
        {
            SetState(PlayerState.Run);
        }
        // Default to Idle if no other conditions are met
        else if (playerController.isGrounded)
        {
            SetState(PlayerState.Idle);
        }
    }


    private void SetState(PlayerState newState)
    {
        if (currentState != newState)
        {
            currentState = newState;
            UpdateAnimator();
        }
    }

    private void UpdateAnimator()
    {
        // Set Animator parameters based on the current state
        animator.SetBool("isIdle", currentState == PlayerState.Idle);
        animator.SetBool("isRunning", currentState == PlayerState.Run);
        animator.SetBool("isJumping", currentState == PlayerState.Jump);
        animator.SetBool("isDashing", currentState == PlayerState.Dash);
        animator.SetBool("isShootingIdle", currentState == PlayerState.ShootIdle);
        animator.SetBool("isShootingRunning", currentState == PlayerState.ShootRunning);
        animator.SetBool("isTakingDamage", currentState == PlayerState.Damage);
    }

    public void TriggerDamage()
    {
        SetState(PlayerState.Damage);
        // Optional: Return to Idle or previous state after a delay
        Invoke("ResetToIdle", 0.5f); // Adjust delay as needed
    }

    private void ResetToIdle()
    {
        SetState(PlayerState.Idle);
    }
}
