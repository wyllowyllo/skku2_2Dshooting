using UnityEngine;

public class ChasingEnemy : StraightEnemy
{
    private GameObject _playerObject;
    private void Start()
    {
        _playerObject=GameObject.FindWithTag("Player");
    }
    private void Update()
    {

        Move();  
    }

    protected sealed override void Move()
    {
        if(_playerObject==null)
        {
            base.Move();
            return;
        }

        Vector2 dir = (_playerObject.transform.position - transform.position).normalized;

        Vector2 currentPos = transform.position;
        Vector2 nextPos = dir * _moveSpeed * Time.deltaTime;
        transform.position = currentPos + nextPos;


    }
}
