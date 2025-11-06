using UnityEngine;

public class CyclonBullet:Bullet
{
    [Header("사이클론 회전")]
    [SerializeField] private float _radius = 0.5f;
    [SerializeField] private float _endRadius = 3f;
  

    private float _timer;
    private Vector2 _startPos;
    private void Start()
    {
        _startPos = transform.position;
        _timer = 0f;
    }
    private void Update()
    {
        _timer += Time.deltaTime;
        BulletMove();
    }

    protected sealed override void BulletMove()
    {
        Vector2 forward = Vector2.up;
        

        // 가속 로직
        float speedDelta = (lastBulletSpeed - firstBulletSpeed) / totalAccelTime;
        bulletSpeed += speedDelta * Time.deltaTime;
        bulletSpeed = Mathf.Min(bulletSpeed, lastBulletSpeed);

        //반지름 증가
        float radiusDelta = (_endRadius - _radius) / totalAccelTime;
        _radius += radiusDelta * Time.deltaTime;
        _radius = Mathf.Min(_radius, _endRadius);

        float theta = 2f * Mathf.PI  * _timer; //시간에 따라 각도 증가

        Vector2 pos =
            _startPos
            + forward * (bulletSpeed * _timer)
            + new Vector2(_radius * Mathf.Cos(theta), _radius * Mathf.Sin(theta));

        transform.position = pos;
    }
}

