using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    [Header("Player Detection & Attack")]
    [SerializeField] private float attackRange = 1.5f;
    [SerializeField] private float attackCooldown = 1.0f;
    [SerializeField] private int attackDamage = 10;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private Transform player;

    [Header("Enemy Stats")]
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private float knockbackForce = 5f;

    [Header("Effects & Animation")]
    [SerializeField] private Animator animator;

    private int currentHealth;
    private float lastAttackTime;
    private bool isDead = false;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (isDead || player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);
        if (distance <= attackRange && Time.time >= lastAttackTime + attackCooldown)
        {
            Attack();
        }
    }

    private void Attack()
    {
        lastAttackTime = Time.time;

        if (animator != null)
        {
            animator.SetTrigger("ATTACK");
        }

        // Gây sát thương nếu player trong vùng
        Collider2D hit = Physics2D.OverlapCircle(transform.position, attackRange, playerLayer);
        if (hit != null && hit.CompareTag("Player"))
        {
            PlayerController playerController = hit.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.TakeDamage(attackDamage);
                playerController.ApplyKnockback(transform.position);
            }
        }
    }

    public void TakeDamage(int amount, Vector2 sourcePosition)
    {
        if (isDead) return;

        currentHealth -= amount;

        if (animator != null)
            animator.SetTrigger("HIT");

        // Knockback
        Vector2 direction = (Vector2)transform.position - sourcePosition;
        direction.Normalize();
        rb.AddForce(direction * knockbackForce, ForceMode2D.Impulse);


        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
        if (animator != null)
            animator.SetTrigger("DEAD");

        rb.linearVelocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Kinematic;
        Observer.Notify("OnAddScore", 50);
        Destroy(gameObject, 1.5f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
