using UnityEngine;

public class AttackSpeedItem : ItemBase
{


    protected override void ApplyEffect(GameObject target)
    {
        PlayerFire playerFire = target.GetComponent<PlayerFire>();
        if (playerFire != null)
            playerFire.FireRateUp(increment);
    }

   

   
}
