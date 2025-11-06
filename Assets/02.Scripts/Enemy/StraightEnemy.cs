using UnityEngine;

public class StraightEnemy : Enemy
{


    [Header("이동 방향")]
    private Vector2 _moveDir;

    public float Health { get => _health; }

    private  void Start()
    {
        _moveDir = Vector2.down;
    }
    private void Update()
    {
        Move();
    }

    protected override void Move()
    {
        Vector2 currentPos = transform.position;
        Vector2 nextPos = _moveDir * _moveSpeed * Time.deltaTime;
        transform.position = currentPos + nextPos;
    }

    public override void Hit(in float damage)
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
