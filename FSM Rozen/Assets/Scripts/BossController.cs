using UnityEngine;
using System.Collections;

public class BossController : MonoBehaviour
{
    [Header("Boss Settings")]
    public int maxHealth = 1000;
    public float attackCooldown = 1f; // Time between attacks
    public float enragedAttackCooldown = 0.5f; // Faster attack in enraged state

    [Header("Projectile Settings")]
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float projectileSpeed = 5f;

    private int currentHealth;
    private float nextAttackTime;
    private BossState currentState;

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
        StartCoroutine(FSM());
    }

    IEnumerator FSM()
    {
        while (currentState != BossState.Defeated)
        {
            switch (currentState)
            {
                case BossState.Idle:
                    yield return IdleBehavior();
                    break;
                case BossState.Phase1Attack:
                    yield return Phase1AttackBehavior();
                    break;
                case BossState.Phase2Attack:
                    yield return Phase2AttackBehavior();
                    break;
                case BossState.Enraged:
                    yield return EnragedBehavior();
                    break;
            }
        }
    }

    IEnumerator IdleBehavior()
    {
        Debug.Log("Boss is idle...");
        yield return new WaitForSeconds(2f); // Wait before transitioning to Phase 1
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
                //FireProjectile();
                FireBurstProjectiles();
                nextAttackTime = Time.time + enragedAttackCooldown;
                
            }
            yield return null;
        }
        currentState = BossState.Defeated;
    }

    private void FireProjectile()
    {
        Debug.Log("Boss fires a projectile...");
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.left * projectileSpeed; // Fire towards the player
        Destroy(projectile, 5f);
    }

    private void FireBurstProjectiles()
    {
        Debug.Log("Boss fires burst projectiles...");
        for (int i = -1; i <= 1; i++) // Fire 3 projectiles in a spread
        {
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(-1, i * 0.5f).normalized * projectileSpeed;
            Destroy(projectile, 5f);
        }
    }

    public void TakeDamage(int damage)
    {
        if (currentState == BossState.Defeated) return;

        currentHealth -= damage;
        Debug.Log($"Boss takes {damage} damage. Health: {currentHealth}");

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
        Destroy(gameObject, 2f); // Optional: Destroy the boss after some delay
    }
}
