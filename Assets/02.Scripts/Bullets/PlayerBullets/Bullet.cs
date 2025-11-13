using UnityEngine;


public enum EBulletType
{
    Normal=0,
    Sub=1,
    Sin=2,
    Cyclon=3,
    Micro=4
}
public class Bullet : MonoBehaviour
{
    [Header("기본 스텟")]
    [SerializeField] protected EBulletType bulletType;
    [SerializeField] protected float bulletDamage;
    [SerializeField] protected float bulletSpeed;
    [SerializeField] protected float firstBulletSpeed = 1f;
    [SerializeField] protected float lastBulletSpeed = 7f;
    [SerializeField] protected float totalAccelTime = 1.2f;


    private void Start()
    {
        bulletSpeed = firstBulletSpeed;
    }
    private void Update()
    {
        BulletMove();
        
    }

    protected virtual void BulletMove()
    {
        //방향을 구한다
        Vector2 direction = Vector2.up;

        float speedDelta = (lastBulletSpeed - firstBulletSpeed) / totalAccelTime;
        bulletSpeed += speedDelta * Time.deltaTime;
        bulletSpeed = Mathf.Min(bulletSpeed, lastBulletSpeed);

        //새로운 위치는 = 현재 위치 + 방향 * 속력 * 시간
        Vector2 position = transform.position;
        Vector2 newPosition = position + direction * bulletSpeed * Time.deltaTime;
        transform.position = newPosition;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy")) return;

      
        Enemy enemy = collision.GetComponent<Enemy>();

        if(enemy != null)
            enemy.Hit(bulletDamage);

        gameObject.SetActive(false);
    }

    
}