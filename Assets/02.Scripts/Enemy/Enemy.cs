using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("기본스텟")]
    [SerializeField] protected float damage = 1f;
    [SerializeField] protected float health = 100f;
    

    [Header("플래그 변수")]
    protected bool isDead = false;

    public float Health { get => health; }

 

    public void Hit(float damage)
    {

        if (isDead) return;

        health -=damage;

        if(health<=0)
        {
            isDead = true;
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        
        Player player = collision.GetComponent<Player>();

        if(player != null)
            player.Hit(damage);

        Destroy(gameObject);    

    }


}
