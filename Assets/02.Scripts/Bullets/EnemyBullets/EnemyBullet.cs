using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private float _damage;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        Player player = collision.GetComponent<Player>();

        if (player != null)
            player.Hit(_damage);

        gameObject.SetActive(false);
    }
}
