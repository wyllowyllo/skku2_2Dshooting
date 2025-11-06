using UnityEngine;

public abstract class Enemy:MonoBehaviour
{
    [Header("기본스텟")]
    [SerializeField] protected float _damage = 1f;
    [SerializeField] protected float _health = 100f;
    [SerializeField] protected float _moveSpeed = 1f;

    [Header("플래그 변수")]
    [SerializeField] protected bool _isDead = false;

    protected abstract void Move();
    public abstract void Hit(in float damage);
}
