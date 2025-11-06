

using UnityEngine;

public class BulletTraceMovement : Movement
{
    [SerializeField] private float _bulletTraceSpeed = 5f;
    [SerializeField] private float _scanRange = 1.5f;

    private GameObject _playerObject;
    private RaycastHit2D _hit;
    private float _applySpeed;

    private void Start()
    {
        //캐싱 : 자주 쓰는 데이터를 미리 가까운 곳에 저장해두고 참조하는 것

        _playerObject = GameObject.FindWithTag("Player");
    }
    private void Update()
    {
        Scan();
        Move();
    }

    private void Scan()
    {
        _hit = Physics2D.CircleCast(transform.position, _scanRange, Vector2.up);
    }
    protected override void Move()
    {
        if (_playerObject == null)
            return;

        Vector2 dir=Vector2.zero;
        

        if (_hit.collider != null && _hit.collider.CompareTag("PlayerBullet"))
        {
            dir = (_hit.transform.position - transform.position).normalized;
            _applySpeed = _bulletTraceSpeed;
        }
        else
        {
            dir = (_playerObject.transform.position - transform.position).normalized;
            _applySpeed = moveSpeed;
        }
            

        Vector2 currentPos = transform.position;
        Vector2 nextPos = dir * _applySpeed * Time.deltaTime;


        float rotateAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotateAngle);
        transform.position = currentPos + nextPos;
    }
}