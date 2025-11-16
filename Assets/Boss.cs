using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Boss : MonoBehaviour
{
    [Header("이동 관련 변수")]
    [SerializeField] private float _moveDuration = 1.5f;
  

    [Header("스킬 관련 변수")]
    [SerializeField] private float _skillDuration = 10f;
    [SerializeField] private float _skillCoolTime = 3f;
    [SerializeField] private float _shotInterval = 0.5f;
    [SerializeField] private int _bulletCntForShot = 12; // 스킬 샷 당 총알 개수
    [SerializeField] private float _bulletAngleOffset = 10f; // 스킬 샷 당 각도 offset

    [Header("총알 관련 변수")]
    [SerializeField] private float _bulletSpeed = 5f;
    [SerializeField] private GameObject _bulletPrefab;

    private float _bossSizeFactor = 1.5f;
    private Vector3 _bossPosition;

    private float _skillTimer = 0f;
    private bool _isShooting = false;
    private void Awake()
    {
        Vector3 spawnPosition = transform.position;

        float targetY = BoardBounds.Instance.BoardTopY - (transform.localScale.y * _bossSizeFactor);
        _bossPosition = new Vector3(spawnPosition.x, targetY, spawnPosition.z);
    }
    private void Start()
    {
        StartCoroutine(Appear());
    }

    private void Update()
    {
        if (_isShooting) return;

        _skillTimer += Time.deltaTime;
        if (_skillTimer >= _skillCoolTime)
        {
            StartCoroutine(CycloneShot());
           
        }
    }
    private IEnumerator Appear()
    {
        float timer = 0f;

        Vector3 startPos = transform.position;
        Vector3 targetPos = _bossPosition;

        while (timer <= _moveDuration)
        {
            timer += Time.deltaTime;

            float t = timer / _moveDuration;
            transform.position = Vector3.Lerp(startPos, targetPos, t);

            yield return null;
        }

        transform.position = targetPos;
        
    }

    private IEnumerator CycloneShot()
    {
        _isShooting = true;

     
        float angleStep = 360f / _bulletCntForShot; // 총알 간격(각도)
        float startAngle = 0f; 
        float _timer = 0f; // 경과 시간

        while (_timer < _skillDuration)
        {
            for (int i = 0; i < _bulletCntForShot; i++)
            {
                float angle = startAngle + (i * angleStep);
                float angleRad = angle * Mathf.Deg2Rad;

                // 날아갈 방향 설정
                Vector2 direction = new Vector2(Mathf.Cos(angleRad), Mathf.Sin(angleRad));

               
                GameObject bullet = Instantiate(_bulletPrefab, transform.position, Quaternion.identity);
                Rigidbody2D bulletRigid = bullet.GetComponent<Rigidbody2D>();

                if (bulletRigid != null)
                {
                    bulletRigid.linearVelocity = direction * _bulletSpeed; 
                }
            }

            startAngle += _bulletAngleOffset; // 바람개비 회전 효과를 위해 각도 증가
            _timer += _shotInterval;
            yield return new WaitForSeconds(_shotInterval); 
        }

        _skillTimer = 0f;
        _isShooting = false;
    }
}
