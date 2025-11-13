
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 타겟의 이전 위치 기록을 따라다니며 적을 공격하는 펫입니다.
/// </summary>
public class FollowPet : PetBase
{
    private float _timer = 0f;
    private Vector2 _targetPosition = Vector2.zero;
    

    private void Start()
    {
        _targetPosRecords = new Queue<Vector2>();

        if (target != null)
        {
            _targetPosition= transform.position;
        }
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= shotInterval)
        {
            Fire();
            _timer = 0f;
        }

        RecordTargetPosition();
        FollowTarget();
    }

    public void SetTargetToFollow(Transform targetTr)
    {
        target = targetTr;
    }
    private void RecordTargetPosition()
    {
        if (target == null) return;
        
        if (!_targetPosRecords.Contains(target.position))
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