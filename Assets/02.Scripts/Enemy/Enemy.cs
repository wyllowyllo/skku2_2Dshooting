using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

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
    [SerializeField] private int _killScore = 100;

    [Header("플래그 변수")]
    private bool _isDead = false;
    
    [Header("폭발 프리펩")]
    [SerializeField] private GameObject _explosionPrefab;

    private ScoreManager _scoreManager;
    private DropItem _dropItem;
    private Animator _animator;
    
    public float Health { get => _health; }


    private void Start()
    {
        _scoreManager = FindAnyObjectByType<ScoreManager>();
        _dropItem = GetComponent<DropItem>();
        _animator = GetComponent<Animator>();
    }
    public void Hit(float damage)
    {

        if (_isDead) return;

        _health -= damage;
        if(_animator!=null) _animator.SetTrigger("Hit");
        
        if(_health<=0)
        {
            
            Death();
            Destroy(gameObject);
        }
    }

    private void Death()
    {
        _isDead = true;
        
        if (_dropItem != null)
            _dropItem.Drop();

        MakeExplosionEffect();
       
        _scoreManager?.AddScore(_killScore); 
    }

   
    private void MakeExplosionEffect()
    {
        if (_explosionPrefab == null) return;
        
        Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
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
