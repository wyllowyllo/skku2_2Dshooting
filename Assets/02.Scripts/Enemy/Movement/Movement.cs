

using UnityEngine;

public abstract class  Movement : MonoBehaviour
{
    [Header("방향&속도")]
    [SerializeField] protected float moveSpeed = 1f;
    protected Vector2 moveDir;

    protected abstract void Move();
   
}
