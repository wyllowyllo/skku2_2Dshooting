using UnityEngine;

public class MoveSpeedItem : ItemBase
{
    
    protected override void ApplyEffect(GameObject target)
    {
        PlayerMove playerMove = target.GetComponent<PlayerMove>();
        if (playerMove != null)
            playerMove.SpeedUp(increment);
    }
   
    
}
