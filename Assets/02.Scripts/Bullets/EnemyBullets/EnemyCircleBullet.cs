using UnityEngine;



public class EnemyCircleBullet : MonoBehaviour
{
    [Header("기본 스텟")]
    [SerializeField] private float _bulletDamage;
    [SerializeField] private float _bulletSpeed;

    private Transform _target;
    private Vector2 _direction;

    private void Start()
    {
        _target = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (_target != null)
            _direction = (_target.position - transform.position).normalized;
        else
            _direction = Vector2.down;
    }
    private void Update()
    {
        BulletMove();

    }

    private void BulletMove()
    {
        Vector2 curPosition=transform.position;
        Vector2 newPosition = curPosition + _direction * _bulletSpeed * Time.deltaTime;
        transform.position = newPosition;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        Player player = collision.GetComponent<Player>();

         if (player != null)
            player.Hit(_bulletDamage);

        Destroy(gameObject);
    }
}