using UnityEngine;

public class HealthItem : ItemBase
{
    protected override void ApplyEffect(GameObject target)
    {
        Player player = target.GetComponent<Player>();
        if (player != null)
            player.Heal(increment);
    }

   
}
