using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float fallMultiplier = 2.5f; // Increases gravity when falling
    public float lowJumpMultiplier = 2f; // Makes short jumps feel snappier

    [Header("Shooting Settings")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 10f;
    public float fireRate = 0.5f;

    [Header("Dash Settings")]
    public float dashSpeed = 20f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;

    [Header("Health Settings")]
    public int maxHealth = 100; // Maximum health
    public int currentHealth;

    [Header("Damage Settings")]
    public float invincibilityDuration = 1.5f; // Time the player is invincible after taking damage
    private bool isInvincible = false;

    public Rigidbody2D rb;
    public bool isGrounded;
    public bool isDashing;
    private bool canDash = true;
    private float nextFireTime = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth; // Initialize health
    }

    void Update()
    {
        if (!isDashing)
        {
            HandleMovement();
            HandleJump();
        }
        HandleShooting();
        HandleDash();
        ApplyBetterJumpingPhysics();
    }

    void HandleMovement()
    {
        float moveInput = Input.GetAxis("Horizontal");
        Vector2 movement = new Vector2(moveInput * moveSpeed, rb.velocity.y);
        rb.velocity = movement;

        // Flip player sprite based on movement direction by adjusting scale
        if (moveInput > 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (moveInput < 0)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }

    void HandleJump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
        }
    }

    void ApplyBetterJumpingPhysics()
    {
        if (rb.velocity.y < 0) // Falling
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump")) // Short jump
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    void HandleShooting()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        bulletRb.velocity = new Vector2(transform.localScale.x * bulletSpeed, 0);
        Destroy(bullet, 2f);
    }

    void HandleDash()
    {
        if (Input.GetKeyDown(KeyCode.E) && canDash && !isDashing)
        {
            float hori = Input.GetAxisRaw("Horizontal"); // Get horizontal direction
            float vert = Input.GetAxisRaw("Vertical");   // Get vertical direction

            if (hori != 0 || vert != 0)
            {
                StartCoroutine(Dash(new Vector2(hori, vert)));
            }
        }
    }

    IEnumerator Dash(Vector2 direction)
    {
        canDash = false;
        isDashing = true;

        Vector2 dashDirection = direction.normalized; // Normalize the direction
        float dashEndTime = Time.time + dashDuration;

        while (Time.time < dashEndTime)
        {
            transform.Translate(dashDirection * dashSpeed * Time.deltaTime);
            yield return null; // Wait for the next frame
        }

        isDashing = false;
        yield return new WaitForSeconds(dashCooldown); // Wait for cooldown
        canDash = true;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    public void TakeDamage(int damage)
    {
        if (isInvincible) return;

        currentHealth -= damage;
        Debug.Log($"Player takes {damage} damage. Health: {currentHealth}");

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Debug.Log("Player is defeated!");
            Die();
        }
        else
        {
            StartCoroutine(InvincibilityCoroutine());
        }
    }

    IEnumerator InvincibilityCoroutine()
    {
        isInvincible = true;
        // Add visual feedback for invincibility (e.g., flashing sprite)
        yield return new WaitForSeconds(invincibilityDuration);
        isInvincible = false;
    }

    private void Die()
    {
        // Add behavior for when the player is defeated
        Debug.Log("Game Over");
        // Optional: Restart the level or end the game
    }
}
