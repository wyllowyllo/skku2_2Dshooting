using UnityEngine;

public abstract class ItemBase:MonoBehaviour
{
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
