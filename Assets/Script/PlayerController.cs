using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private Animator animator;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private GameObject attackHitbox;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private float attackDuration = 0.2f;
    [SerializeField] private float nockbackForce = 5f;
    private bool isRUNorJUMP = false;
    private bool isAttacking = false;
    public int health = 100;
    [SerializeField] private Collider2D attackCollider;
    private bool isHurt = false;
    [SerializeField] private float hurtDuration = 0.4f;
    private bool isDead = false;
    private bool isRunningSoundPlaying = false;
    [SerializeField] private Slider healthSlider;


    public void TakeDamage(int amount)
    {
        if (isHurt) return;
        isHurt = true;
        health -= amount;
        Debug.Log("Player hit! HP: " + health);
        SoundManager.Instance.Play("PlayerHIT");
        animator.SetTrigger("hit");
        StartCoroutine(RecoverFromHurt());
        if (healthSlider != null)
        {
            healthSlider.value = health;
        }


        if (health <= 0)
        {
            StartCoroutine(Die());
        }
    }
    private IEnumerator RecoverFromHurt()
    {
        yield return new WaitForSeconds(hurtDuration);
        isHurt = false;
    }

     System.Collections.IEnumerator Die()
    {
        isDead = true;
        rb.linearVelocity = Vector2.zero;
        animator.SetTrigger("DEAD");
        yield return new WaitForSeconds(2f);
        gameOverPanel.SetActive(true);
        yield return new WaitForSeconds(1f);
        SoundManager.Instance.Play("GameOver");
            Time.timeScale = 0f;
    }
    
    private Rigidbody2D rb;
    private bool isGrounded;
    private Vector2 moveInput;
    private SpriteRenderer spriteRenderer;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D component not found on PlayerController.");
        }
    }
    private void Update()
    {
        if (isDead)
        {
            return;
        }
        if (isHurt)
        {
            return;
        }


        if (Input.GetKey(KeyCode.A))
        {
            moveInput = Vector2.left;

            Move();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            moveInput = Vector2.right;

            Move();
        }
        else
        {
            moveInput = Vector2.zero;
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);

            if (isRunningSoundPlaying)
            {
                isRunningSoundPlaying = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                Jump();
            }
        }
        checkGround();
        Animation();
    }
    private void Move()
    {
        if (!isRunningSoundPlaying)
        {
            SoundManager.Instance.Play("RUN");
            isRunningSoundPlaying = true;
        }

        rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocity.y);

        if (moveInput.x < 0)
        {
            if (transform.localScale.x > 0)
            {
                Vector3 scale = transform.localScale;
                scale.x *= -1f;
                transform.localScale = scale;
            }
        }
        else if (moveInput.x > 0)
        {
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x);
            transform.localScale = scale;
        }
    }
    private void Jump()
    {
        SoundManager.Instance.Play("JUPM");
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    }
    void checkGround()
    {
        // Kiểm tra va chạm với mặt đất bằng 2 tia ở hai bên chân để nhảy sát bờ
        Vector2 leftFoot = (Vector2)transform.position + Vector2.left * 0.25f;
        Vector2 rightFoot = (Vector2)transform.position + Vector2.right * 0.25f;
        RaycastHit2D hitLeft = Physics2D.Raycast(leftFoot, Vector2.down, 0.5f, groundLayer);
        RaycastHit2D hitRight = Physics2D.Raycast(rightFoot, Vector2.down, 0.5f, groundLayer);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.5f, groundLayer);
        isGrounded = hitLeft.collider != null || hitRight.collider != null || hit.collider != null;
        Debug.DrawRay(leftFoot, Vector2.down * 0.5f, Color.red);
        Debug.DrawRay(rightFoot, Vector2.down * 0.5f, Color.green);
        Debug.DrawRay(transform.position, Vector2.down * 0.5f, Color.blue);

        // Nếu không chạm đất, kiểm tra xem có đang nhảy hay không
        if (!isGrounded )
        {
            isGrounded = false;
        }
        else
        {
            isGrounded = true;
        }
    }
    void Animation()
    {
        if (rb.linearVelocity.x != 0)
        {
            animator.SetBool("RUN", true);
            isRUNorJUMP = true;

        }
        else
        {
            animator.SetBool("RUN", false);
            isRUNorJUMP = false;
        }
        if (rb.linearVelocity.y != 0)
        {
            animator.SetBool("JUMP", true);
            isRUNorJUMP = true;
        }
        else
        {
            animator.SetBool("JUMP", false);
            isRUNorJUMP = false;
        }

        if (isRUNorJUMP)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0) && !isAttacking)
        {
            StartCoroutine(Attack1());
        }
        if (Input.GetMouseButtonDown(1) && !isAttacking)
        {
            StartCoroutine(Attack2());

        }

    }
    private System.Collections.IEnumerator Attack1()
    {
        SoundManager.Instance.Play("ATTACK1");
        isAttacking = true;
        animator.SetBool("ATTACK1", true);
        attackCollider.enabled = false;
        yield return null;
        attackCollider.enabled = true;

        yield return new WaitForSeconds(attackDuration);
        attackCollider.enabled = false;
        animator.SetBool("ATTACK1", false);
        isAttacking = false;
    }

    private System.Collections.IEnumerator Attack2()
    {
        SoundManager.Instance.Play("ATTACK2");
        isAttacking = true;
        animator.SetBool("ATTACK2", true);
        attackCollider.enabled = false;
        yield return null;
        attackCollider.enabled = true;

        yield return new WaitForSeconds(attackDuration);
        attackCollider.enabled = false;
        animator.SetBool("ATTACK2", false);
        isAttacking = false;
    }
    public void ApplyKnockback(Vector2 source)
    {
        Vector2 direction = (transform.position - (Vector3)source).normalized;
        rb.AddForce(direction * nockbackForce, ForceMode2D.Impulse);
    }

}
