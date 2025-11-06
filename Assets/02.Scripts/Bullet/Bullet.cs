using UnityEngine;


public enum BulletType
{
    BT_NORMAL=1,
    BT_MINI=2,
    BT_S=3,
    BT_CYCLON=4,
}
public class Bullet : MonoBehaviour
{
    [Header("기본 스텟")]
    [SerializeField] private BulletType _bulletType;
    [SerializeField] protected float _bulletDamage;
    [SerializeField] protected float _bulletSpeed;
    [SerializeField] protected float _firstBulletSpeed = 1f;
    [SerializeField] protected float _lastBulletSpeed = 7f;
    [SerializeField] protected float _totalAccelTime = 1.2f;


    protected virtual void Start()
    {
        _bulletSpeed = _firstBulletSpeed;
    }
    protected virtual void Update()
    {
        BulletMove();
        
    }

    protected virtual void BulletMove()
    {
        //방향을 구한다
        Vector2 direction = Vector2.up;

        float speedDelta = (_lastBulletSpeed - _firstBulletSpeed) / _totalAccelTime;
        _bulletSpeed += speedDelta * Time.deltaTime;
        _bulletSpeed = Mathf.Min(_bulletSpeed, _lastBulletSpeed);

        //새로운 위치는 = 현재 위치 + 방향 * 속력 * 시간
        Vector2 position = transform.position;
        Vector2 newPosition = position + direction * _bulletSpeed * Time.deltaTime;
        transform.position = newPosition;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy")) return;

      
        StraightEnemy enemy = collision.GetComponent<StraightEnemy>();

        if(enemy!=null)
            enemy.Hit(_bulletDamage);

        Destroy(gameObject);
    }

    
}
