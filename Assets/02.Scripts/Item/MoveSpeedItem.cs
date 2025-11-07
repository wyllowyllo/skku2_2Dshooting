using UnityEngine;

public class MoveSpeedItem : ItemBase
{
    
    protected override void ApplyEffect(GameObject target)
    {
        PlayerMove playerMove = target.GetComponent<PlayerMove>();
        if (playerMove != null)
            playerMove.SpeedUp(increment);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.CompareTag("Player"))
            return;

        ApplyEffect(collision.gameObject);


        Destroy(gameObject);
    }

    
}
