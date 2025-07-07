using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    public float moveSpeed = 2f;
    public Transform pointA;
    public Transform pointB;
    private Vector3 target;
    private Animator animator;
    private Rigidbody2D rb;
    public float knockbackForce = 5f;

    public int health = 100;
    private bool isDead = false;

    public void TakeDamage(int amount, Vector2 sourcePosition)
    {
        health -= amount;
        Debug.Log("Enemy hit! HP: " + health);
        ApplyKnockback(sourcePosition);
        SoundManager.Instance.Play("EnemyHIT");
        animator.SetTrigger("hit");
        if (health <= 0)
        {
            StartCoroutine(Die());
        }
    }

    System.Collections.IEnumerator Die()
    {
        isDead = true;
        animator.SetBool("isDead", true);
        yield return new WaitForSeconds(1f);
        Observer.Notify("OnAddScore", 50);
        Destroy(gameObject,1.5f);
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        target = pointB.position;
    }

    void Update()
    {
        Move();
    }
    void ApplyKnockback(Vector2 source)
    {
        Vector2 direction = (transform.position - (Vector3)source).normalized;
        rb.AddForce(direction * knockbackForce, ForceMode2D.Impulse);
    }


    void Move()
    {
        if (isDead) return;
        transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target) < 0.1f)
        {
            target = target == pointA.position ? pointB.position : pointA.position;
            Flip();
        }
    }

    void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            if (player != null)
            {
                player.TakeDamage(10); 
                player.ApplyKnockback(transform.position);
            }
        }
    }

}
