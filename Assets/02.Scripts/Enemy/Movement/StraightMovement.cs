

using UnityEngine;

public class StraightMovement : Movement
{
    private void Start()
    {
        moveDir = Vector2.down;
    }

    private void Update()
    {
        Move();
    }

    protected override void Move()
    {
        Vector2 currentPos = transform.position;
        Vector2 nextPos = moveDir * moveSpeed * Time.deltaTime;
        transform.position = currentPos + nextPos;
    }
}
