using UnityEngine;

public class SwordHitbox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy1 enemy = collision.GetComponent<Enemy1>();
            Enemy2 enemy2 = collision.GetComponent<Enemy2>();
            if (enemy != null)
            {
                enemy.TakeDamage(10, transform.position);
            }
            else if (enemy2 != null)
            {
                enemy2.TakeDamage(10, transform.position);
            }
    }
    }
}
