

using UnityEngine;

public abstract class EnemyAttack : MonoBehaviour
{
    [Header("기본 스텟")]
    [SerializeField] protected float fireRate = 1f;
    protected float cooldownTime = 0f;

    protected abstract void FireCoolTimer();
    protected abstract void Fire();

   
}
