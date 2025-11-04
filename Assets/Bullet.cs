using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("기본 스텟")]
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private float _firstBulletSpeed = 1f;
    [SerializeField] private float _lastBulletSpeed = 7f;
    [SerializeField] private float _accelation = 1.2f;

    private void Start()
    {
        _bulletSpeed = _firstBulletSpeed;
    }
    private void Update()
    {
        //방향을 구한다
        Vector2 direction= Vector2.up;

        /* float speedDelta= (_lastBulletSpeed - _firstBulletSpeed) / _accelation;
         _bulletSpeed += speedDelta * Time.deltaTime;*/
        _bulletSpeed += Time.deltaTime * _accelation;
        _bulletSpeed= Mathf.Min(_bulletSpeed, _lastBulletSpeed);

        //새로운 위치는 = 현재 위치 + 방향 * 속력 * 시간
        Vector2 position = transform.position;
        Vector2 newPosition= position + direction * _bulletSpeed *Time.deltaTime;
        transform.position = newPosition;

        //transform.Translate(Vector2.up * _bulletSpeed*Time.deltaTime);
    }
}
