using UnityEngine;

public enum EnemyType
{
    ET_Straight = 0,
    ET_Trace = 1,
}

public class Enemy : MonoBehaviour
{
    [Header("기본스텟")]
    [SerializeField] private EnemyType _enemyType;
    [SerializeField] private float _damage = 1f;
    [SerializeField] private float _health = 100f;
    

    [Header("플래그 변수")]
    private bool _isDead = false;

    public float Health { get => _health; }

 

    public void Hit(float damage)
    {

        if (_isDead) return;

        _health -=damage;

        if(_health<=0)
        {
            _isDead = true;
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        
        Player player = collision.GetComponent<Player>();

        if(player != null)
            player.Hit(_damage);

        Destroy(gameObject);    

    }


}
