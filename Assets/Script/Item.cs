using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    [SerializeField] private int scoreValue = 50;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Gửi thông báo cộng điểm
            SoundManager.Instance.Play("Gem");
            Observer.Notify("OnAddScore", scoreValue);
            Destroy(gameObject);
        }
    }
}
