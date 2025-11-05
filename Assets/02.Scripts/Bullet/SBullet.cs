

using System;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;

public class SBullet :Bullet
{
    [Header("S 곡선 그리기")]
    [SerializeField] private float _freqnuency = 2f; // 주기
    [SerializeField] private float _amplitude = 0.5f; // 진폭


    private float _timer;
    private Vector2 _startPos;
    protected override void Start()
    {
        _startPos = transform.position;
        _timer = 0f;
    }

    protected override void Update()
    {
        _timer += Time.deltaTime;
        base.Update();
    }

    protected sealed override  void BulletMove() 
    {
        Vector2 forward = Vector2.up;
        Vector2 right = Vector2.right;

        // 가속 로직
        float speedDelta = (_lastBulletSpeed - _firstBulletSpeed) / _totalAccelTime;
        _bulletSpeed += speedDelta * Time.deltaTime;
        _bulletSpeed = Mathf.Min(_bulletSpeed, _lastBulletSpeed);

        float theta = 2f * Mathf.PI * _freqnuency * _timer; //시간에 따라 각도 증가

        Vector2 pos =
            _startPos
            + forward * (_bulletSpeed * _timer)
            + right * (_amplitude * Mathf.Sin(theta));

        transform.position = pos;
    }
}
