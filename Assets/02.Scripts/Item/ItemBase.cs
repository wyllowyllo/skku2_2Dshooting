using UnityEngine;


public enum EItemType
{
    IT_Health=0,
    IT_MoveSpeed=1,
    IT_ShotSpeed=2,
}
public abstract class ItemBase:MonoBehaviour
{
    [SerializeField] protected EItemType itemType;
    [SerializeField] protected float increment = 1.0f;
    [SerializeField] protected const string targetTag = "Player";
    protected abstract void ApplyEffect(GameObject target);

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag(targetTag))
            return;

        ApplyEffect(collision.gameObject);


        Destroy(gameObject);
    }
}
