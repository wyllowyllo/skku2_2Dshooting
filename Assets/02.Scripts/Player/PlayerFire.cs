using System;
using UnityEngine;

public enum AttackMode
{
   ATK_AUTO = 1,
   ATK_MANUAL = 2,
}
public class PlayerFire : MonoBehaviour
{
    //목표 : 스페이스바를 누르면 총알을 만들어서 발사하고 싶다
    [Header("공격 모드")]
    private AttackMode _attackMode = AttackMode.ATK_MANUAL;
    
    [Header("총구")]
    [SerializeField] private Transform _firePositionL;
    [SerializeField] private Transform _firePositionR;
    [SerializeField] private Transform _firePositionSubL;
    [SerializeField] private Transform _firePositionSubR;

    [Header("연사율")]
    [SerializeField] private float _fireRate = 0.6f;
    [SerializeField] private float _minFireRate = 0.1f;
    
    [Header(("사운드"))]
    [SerializeField] private AudioSource _fireSound;
    
    
    [Header("플레이어 입력 처리 모듈")]
    private InputController _input;
    
    private float _cooldownTime = 0f;


    private void Start()
    {
       _input = GetComponent<InputController>();
    }

    private void Update()
    {
        if (_input == null) return;
        
        //공격 상태(쿨타임, 공격 모드( 관리
       FireCoolTimer();
       SwitchAtkMode();
       
       //공격 로직
       Fire();
       Boom();
    }

    private void Fire()
    {
        if (_cooldownTime < _fireRate) return;
            
        
        if (_input.Fire || _attackMode == AttackMode.ATK_AUTO)
        {
            // 사운드 재생
           PlaySFX();
            
            // 총알 생성
            MakeBullets();
            _cooldownTime = 0f;
        }
    }

    private void PlaySFX()
    {
        _fireSound?.Play();
    }
    private void Boom()
    {
        if (!(_input.Boom)) return;

        Vector3 boomPos = BoardBounds.Instance.BoardCenter;
        PlayerBulletFactory.Instance.MakeBoom(boomPos);
    }
    public void FireRateUp(float increment)
    {
        _fireRate -= increment;
        _fireRate = Mathf.Max(_fireRate, _minFireRate);
    }

    private void MakeBullets()
    {
        //클래스 -> 객체(속성+기능) -> 메모리에 실제로 로드된 객체를 인스턴스라고 한다.
        PlayerBulletFactory.Instance.MakeBullet(EBulletType.Basic, _firePositionL.position);
        PlayerBulletFactory.Instance.MakeBullet(EBulletType.Basic, _firePositionR.position);
        
        PlayerBulletFactory.Instance.MakeBullet(EBulletType.Sub, _firePositionSubL.position);
        PlayerBulletFactory.Instance.MakeBullet(EBulletType.Sub, _firePositionSubR.position);
    }

    private void SwitchAtkMode()
    {
        if(_input.AutoMode)
            _attackMode = AttackMode.ATK_AUTO;

        if(_input.ManualMode)
            _attackMode = AttackMode.ATK_MANUAL;
    }
    private void FireCoolTimer()
    {
        _cooldownTime += Time.deltaTime;
    }
}
