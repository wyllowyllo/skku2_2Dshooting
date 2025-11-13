
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 모든 펫의 기본 동작을 정의하는 추상 클래스
/// </summary>
public abstract class PetBase : MonoBehaviour
{
    [Header("총알 프리펩")]
    [SerializeField] protected GameObject bulletPrefab = null;
    
    [Header("공격, 팔로우 딜레이")]
    [SerializeField] protected float shotInterval = 2f;
    [SerializeField] protected int followDelay = 7;
    
    [Header("팔로우 타겟")]
    [SerializeField] protected Transform target = null;
    
    protected Queue<Vector2> _targetPosRecords;

   
    protected abstract void FollowTarget();
    protected abstract void Fire();
}