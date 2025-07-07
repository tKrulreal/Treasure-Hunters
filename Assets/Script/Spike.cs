using UnityEngine;


public class Spike : MonoBehaviour
{
    [SerializeField] private int damage = 10; 
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            if (player != null)
            {
                player.TakeDamage(damage); 
                player.ApplyKnockback(transform.position); 
                Debug.Log("Player hit by spike! Damage: " + damage);
            }
        }
    }
}
