using UnityEngine;

public class EnemyFire : EnemyAttack
{
    [Header("총알 프리펩")]
    [SerializeField] private GameObject _bulletPrefab;

    private Vector2 _bulletPosOffsetL=new Vector2(-0.5f,-1f);
    private Vector2 _bulletPosOffsetR = new Vector2(+0.5f, -1f);

    private void Update()
    {
        FireCoolTimer();
        Fire();
    }

    protected override void FireCoolTimer()
    {
        cooldownTime += Time.deltaTime;
    }

    protected override void Fire()
    {
        if (cooldownTime < fireRate)
            return;

        if (_bulletPrefab != null)
        {
            GameObject bulletL= Instantiate(_bulletPrefab);
            GameObject bulletR= Instantiate(_bulletPrefab);

            bulletL.transform.position = (Vector2)transform.position + _bulletPosOffsetL;
            bulletR.transform.position = (Vector2)transform.position + _bulletPosOffsetR;
        }
            
        
        cooldownTime = 0f;
    }
    
    
}
