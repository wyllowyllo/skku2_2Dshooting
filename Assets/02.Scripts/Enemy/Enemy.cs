using UnityEngine;

public enum EEnemyType
{
    ET_Straight = 0,
    ET_Trace = 1,
    ET_Eater = 2,
}

public class Enemy : MonoBehaviour
{
    [Header("기본스텟")]
    [SerializeField] private EEnemyType _enemyType;
    [SerializeField] private float _damage = 1f;
    [SerializeField] private float _health = 100f;
    

    [Header("플래그 변수")]
    private bool _isDead = false;

    private DropItem _dropItem;
    public float Health { get => _health; }


    private void Start()
    {
        _dropItem = GetComponent<DropItem>();
    }
    public void Hit(float damage)
    {

        if (_isDead) return;

        _health -= damage;

        if(_health<=0)
        {
            _isDead = true;

            if (_dropItem != null)
                _dropItem.Drop();

            
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
