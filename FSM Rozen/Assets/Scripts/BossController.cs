using UnityEngine;
using System.Collections;

public class BossController : MonoBehaviour
{
    [Header("Boss Settings")]
    public int maxHealth = 1000;
    public float attackCooldown = 1f; 
    public float enragedAttackCooldown = 0.5f; 

    [Header("Projectile Settings")]
    public GameObject projectilePrefab;
    public Transform[] firePoints; // Array of fire points
    public float projectileSpeed = 5f;

    private int currentHealth;
    private float nextAttackTime;
    private BossState currentState;
    private Animator animator;

    private enum BossState
    {
        Idle,
        Phase1Attack,
        Phase2Attack,
        Enraged,
        Defeated
    }

    void Start()
    {
        currentHealth = maxHealth;
        currentState = BossState.Idle;
        animator = GetComponent<Animator>();
        StartCoroutine(FSM());
    }

    IEnumerator FSM()
    {
        while (currentState != BossState.Defeated)
        {
            switch (currentState)
            {
                case BossState.Idle:
                    SetPhaseAnimation(1); // temporary fix since i dont have an idle animation

                    yield return IdleBehavior();
                    break;

                case BossState.Phase1Attack:
                    SetPhaseAnimation(1);

                    yield return Phase1AttackBehavior();
                    break;

                case BossState.Phase2Attack:
                    SetPhaseAnimation(2);

                    yield return Phase2AttackBehavior();
                    break;

                case BossState.Enraged:
                    SetPhaseAnimation(3);
                    yield return EnragedBehavior();
                    break;
            }
        }
    }
    private void SetPhaseAnimation(int phase)
    {
        if (animator != null)
        {
            Debug.Log($"Setting phase to {phase}");
            animator.SetInteger("Phase", phase); //doesn't work every time only works for 2 
        }
    }
    IEnumerator IdleBehavior()
    {
        Debug.Log("Boss is idle...");
        yield return new WaitForSeconds(0.2f); // Wait before transitioning to Phase 1
        currentState = BossState.Phase1Attack;
    }

    IEnumerator Phase1AttackBehavior()
    {
        Debug.Log("Boss in Phase 1 Attack...");
        while (currentHealth > maxHealth * 0.5f) // While health is above 50%
        {
            if (Time.time >= nextAttackTime)
            {
                FireProjectile();
                nextAttackTime = Time.time + attackCooldown;
            }
            yield return null;
        }
        currentState = BossState.Phase2Attack;
    }

    IEnumerator Phase2AttackBehavior()
    {
        Debug.Log("Boss in Phase 2 Attack...");
        while (currentHealth > maxHealth * 0.2f) // While health is above 20%
        {
            if (Time.time >= nextAttackTime)
            {
                FireBurstProjectiles();
                nextAttackTime = Time.time + attackCooldown;
            }
            yield return null;
        }
        currentState = BossState.Enraged;
    }

    IEnumerator EnragedBehavior()
    {
        Debug.Log("Boss is enraged!");
        while (currentHealth > 0)
        {
            if (Time.time >= nextAttackTime)
            {
                FireBurstProjectiles();
                nextAttackTime = Time.time + enragedAttackCooldown;
            }
            yield return null;
        }
        currentState = BossState.Defeated;
    }

    private void FireProjectile()
    {
        //Debug.Log("Boss fires a projectile...");
        Transform selectedFirePoint = firePoints[Random.Range(0, firePoints.Length)];
        GameObject projectile = Instantiate(projectilePrefab, selectedFirePoint.position, Quaternion.Euler(0, 0, 90));

        BossBullet bullet = projectile.GetComponent<BossBullet>();

        if (bullet != null)
        {
            switch (currentState)
            {
                case BossState.Phase1Attack:
                    bullet.SetPhaseAnimation("Phase1Bullet");
                    break;
                case BossState.Phase2Attack:
                    bullet.SetPhaseAnimation("Phase2Bullet");
                    break;
                case BossState.Enraged:
                    bullet.SetPhaseAnimation("EnragedBullet");
                    break;
                default:
                    break;
            }
        }

        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.left * projectileSpeed;
        Destroy(projectile, 5f);
    }

    private void FireBurstProjectiles()
    {
        //Debug.Log("Boss fires burst projectiles...");
        Transform selectedFirePoint = firePoints[Random.Range(0, firePoints.Length)];
        for (int i = -1; i <= 1; i++) // Fire 3 projectiles in a spread
        {
            GameObject projectile = Instantiate(projectilePrefab, selectedFirePoint.position, Quaternion.Euler(0, 0, 90));
            BossBullet bullet = projectile.GetComponent<BossBullet>();
            Debug.Log($"Current Boss State: {currentState}");
            // Animation for the bullet
            if (bullet != null)
            {
                Debug.Log("Bullet is not null");
                switch (currentState)
                {
                    case BossState.Phase1Attack:
                        bullet.SetPhaseAnimation("Phase1Bullet");
                        break;
                    case BossState.Phase2Attack:
                        bullet.SetPhaseAnimation("Phase2Bullet");
                        break;
                    case BossState.Enraged:
                        bullet.SetPhaseAnimation("EnragedBullet");
                        break;
                    default:
                        Debug.LogError($"Unhandled state: {currentState}");
                        break;
                }
            }

            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(-1, i * 0.5f).normalized * projectileSpeed;
            Destroy(projectile, 5f);
        }
    }

    public void TakeDamage(int damage)
    {
        if (currentState == BossState.Defeated) return;

        currentHealth -= damage;
        //Debug.Log($"Boss takes {damage} damage. Health: {currentHealth}");

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            currentState = BossState.Defeated;
            DefeatedBehavior();
        }
    }

    private void DefeatedBehavior()
    {
        Debug.Log("Boss is defeated!");
        Destroy(gameObject, 2f);
    }
}
