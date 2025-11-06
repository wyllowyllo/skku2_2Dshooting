

using UnityEngine;

public class TraceMovement : Movement
{
    private GameObject _playerObject;

    private void Start()
    {
        _playerObject = GameObject.FindWithTag("Player");
    }
    private void Update()
    {
        Move();
    }
    protected override void Move()
    {
        if (_playerObject == null)
        {

            return;
        }

        Vector2 dir = (_playerObject.transform.position - transform.position).normalized;

        Vector2 currentPos = transform.position;
        Vector2 nextPos = dir * moveSpeed * Time.deltaTime;


        float rotateAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotateAngle);
        transform.position = currentPos + nextPos;
    }
}