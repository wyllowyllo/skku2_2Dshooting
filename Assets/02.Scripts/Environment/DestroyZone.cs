using UnityEngine;

public class DestroyZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBullet"))
        {
            collision.gameObject.SetActive(false);
            return;
        }
        
        Destroy(collision.gameObject);
    }
}
