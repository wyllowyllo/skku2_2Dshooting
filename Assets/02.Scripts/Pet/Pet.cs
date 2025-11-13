using System;
using UnityEngine;

public class Pet : PetBase
{
    private float _timer = 0f;
    private Vector2 _targetPosition = Vector2.zero;
    private Vector2 _lastTargetPosition = Vector2.zero;
    
    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= shotInterval)
        {
            Fire();
            _timer = 0f;
        }

        LookatTarget();
        FollowTarget();
    }

    private void LookatTarget()
    {
        
        if (Vector2.Distance(target.position, _lastTargetPosition) > 0.001f)
        {
            _targetPosRecords.Enqueue(target.position);
        }
        
        
        if (_targetPosRecords.Count > followDelay)
        {
            _targetPosition = _targetPosRecords.Dequeue();
        }
    }
    protected override void FollowTarget()
    {
        if (target == null) return;
        
        transform.position = _targetPosition;
    }

    protected override void Fire()
    {
        if (bulletPrefab == null) return;
        
        Instantiate(bulletPrefab, transform.position, transform.rotation);
    }
}