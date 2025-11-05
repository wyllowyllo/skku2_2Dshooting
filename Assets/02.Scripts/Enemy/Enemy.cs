using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("기본스텟")]
    [SerializeField] private float _health = 100f;
    [SerializeField] private float _moveSpeed=1f;

    [Header("이동 방향")]
    private Vector2 _moveDir;

    [Header("플래그 변수")]
    private bool _isDead = false;

   
       

    public float Health { get => _health; }

    private void Start()
    {
        _moveDir = Vector2.down;
    }
    private void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector2 currentPos = transform.position;
        Vector2 nextPos = _moveDir * _moveSpeed * Time.deltaTime;
        transform.position = currentPos + nextPos;
    }

    public void OnDamage(in float damage)
    {

        if (_isDead) return;

        _health -=damage;

        if(_health<=0)
        {
            _isDead = true;
            Destroy(gameObject);
        }
    }

    

}
