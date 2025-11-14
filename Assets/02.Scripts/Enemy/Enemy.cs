using System;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public enum EEnemyType
{
    Straight = 0,
    Trace = 1,
    Eater = 2,
}

public class Enemy : MonoBehaviour
{
    [Header("기본스텟")]
    [SerializeField] private EEnemyType _enemyType;
    [SerializeField] private float _damage = 1f;
    [SerializeField] private float _health = 100f;
    [SerializeField] private float _maxHealth = 100f;
    [SerializeField] private int _killScore = 100;

    [Header("플래그 변수")]
    private bool _isDead = false;
    
    [Header("폭발 프리펩")]
    [SerializeField] private GameObject _explosionPrefab;
    
   
    
    private DropItem _dropItem;
    private Animator _animator;
    
    public float Health { get => _health; }


    private void Awake()
    {
        _dropItem = GetComponent<DropItem>();
        _animator = GetComponent<Animator>();
      
        Init();
    }
   
    public void Hit(float damage)
    {

        if (_isDead) return;

        _health -= damage;
       
        
        if(_health<=0)
        {
            
            Death();
            gameObject.SetActive(false);
            _animator.SetTrigger("Reset");
            return;
        }
        if(_animator!=null) _animator.SetTrigger("Hit");
    }

    private void Init()
    {
        _isDead = false;
        transform.rotation = Quaternion.identity;
        _health = _maxHealth;
    }

    private void Death()
    {
        _isDead = true;
        
        if (_dropItem != null)
            _dropItem.Drop();

        MakeExplosionEffect();
       
        ScoreManager.Instance.AddScore(_killScore); // TODO : 적 사망 이벤트 만들고, ScoreManager가 구독하는 방식으로 바꾸기
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

       gameObject.SetActive(false);  

    }


    private void OnEnable()
    {
        Init();
    }
}
