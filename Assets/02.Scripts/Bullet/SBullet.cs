

using System;
using UnityEngine;

public class SBullet :Bullet
{
    Vector2 _startPos;
    float _t;

    protected override void Start()
    {
        _startPos = transform.position;
        _t = 0f;
    }

    protected override void Update()
    {
        _t += Time.deltaTime;

        
        float f = 2f;  // 주기               
        float amp = 0.5f;   // 진폭           
        Vector2 forward = Vector2.up; 
        Vector2 right = Vector2.right;

        // 가속 로직
        float speedDelta = (_lastBulletSpeed - _firstBulletSpeed) / _totalAccelTime;
        _bulletSpeed += speedDelta * Time.deltaTime;
        _bulletSpeed = Mathf.Min(_bulletSpeed, _lastBulletSpeed);

        float theta = 2f * Mathf.PI * f * _t; //시간에 따라 각도 증가

        Vector2 pos =
            _startPos
            + forward * (_bulletSpeed * _t)               
            + right * (amp * Mathf.Sin(theta));          

        transform.position = pos;
    }
}
