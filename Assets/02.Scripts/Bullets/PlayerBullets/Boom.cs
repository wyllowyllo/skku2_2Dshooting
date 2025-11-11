using System;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Boom : MonoBehaviour
{
    [Header("기본스텟")]
    [Tooltip("데미지는 적들을 한번에 죽일 수 있을 만큼 큰 값이어야 합니다.")]
    [SerializeField] private float _damage;
    [SerializeField] private float _existTime = 3f;
    
    [Header("파티클 이펙트")]
    [SerializeField] private GameObject _boomFXPrefab;
    [SerializeField] private float _fxPopUpInterval;
    [SerializeField] private float _minFxPosOffset;
    [SerializeField] private float _maxFxPosOffset;
    
    private float _lifeCoolTime;
    private float _fxCoolTime;

    private void Start()
    {
        _lifeCoolTime = 0f;
        _fxCoolTime = _fxPopUpInterval;
    }
    private void Update()
    {
        if( _lifeCoolTime >= _existTime )
            Destroy(gameObject);
        
        Timer();
        PopFx();
    }

    private void Timer()
    {
        _lifeCoolTime += Time.deltaTime;
        _fxCoolTime += Time.deltaTime;
        
    }

    private void PopFx()
    {
        if (_fxCoolTime < _fxPopUpInterval || _boomFXPrefab == null) return;

        float offsetX =Random.Range(-_minFxPosOffset, _maxFxPosOffset);
        float offsetY = Random.Range(-_minFxPosOffset, _maxFxPosOffset);
        
        Vector2 fxPos = (Vector2)transform.position + new Vector2(offsetX,offsetY);
        Instantiate(_boomFXPrefab, fxPos, Quaternion.identity);
        
        _fxCoolTime = 0f;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy")) return;

      
        Enemy enemy = collision.GetComponent<Enemy>();

        if(enemy != null)
            enemy.Hit(_damage);
        
    }

   
}