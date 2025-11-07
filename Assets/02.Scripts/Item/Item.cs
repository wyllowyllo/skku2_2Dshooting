using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private float _speedIncrement = 1.0f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.CompareTag("Player"))
            return;

        PlayerMove playerMove = collision.GetComponent<PlayerMove>();
        if(playerMove != null)
            playerMove.SpeedUp(_speedIncrement);
            

        Destroy(gameObject);
    }
}
