using System.Collections;
using UnityEngine;


public enum EItemType
{
    IT_Health=0,
    IT_MoveSpeed=1,
    IT_ShotSpeed=2,
}
public abstract class ItemBase:MonoBehaviour
{
    [Header("아이템 타입 &  효과 증분")]
    [SerializeField] protected EItemType itemType;
    [SerializeField] protected float increment = 1.0f;
    
    [Header("아이템 획득 이펙트 프리펩")]
    [SerializeField] protected GameObject itemGetEffectPrefab;
    
    protected const string targetTag = "Player";
    
    protected abstract void ApplyEffect(GameObject target);
   
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag(targetTag))
            return;

        ApplyEffect(collision.gameObject);
        
        PlayVisualEffect();

        Destroy(gameObject);
    }

    protected void PlayVisualEffect()
    {
        if (itemGetEffectPrefab == null) return;
        
        Instantiate(itemGetEffectPrefab, transform.position, Quaternion.identity);
    }

   
}
