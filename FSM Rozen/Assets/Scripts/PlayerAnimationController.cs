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
    public Animator animator; 
    public PlayerController playerController;

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
        if (playerController.isDashing)
        {
            SetState(PlayerState.Dash); //Dash
        }
        else if (!playerController.isGrounded) 
        {
            SetState(PlayerState.Jump); // Jumping
        }
        else if (Input.GetButton("Fire1")) // Shooting
        {
            if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1f)
                SetState(PlayerState.ShootRunning); // Shooting and Running
            else
                SetState(PlayerState.ShootIdle); // Shooting Idle
        }
        else if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1f && playerController.isGrounded) 
        {
            SetState(PlayerState.Run); // Running
        }
        else if (playerController.isGrounded) 
        {
            SetState(PlayerState.Idle); // Default to Idle
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
        //animator.SetBool("isTakingDamage", currentState == PlayerState.Damage);
    }

}
