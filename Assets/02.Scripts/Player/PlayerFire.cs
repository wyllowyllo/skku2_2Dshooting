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
    private AttackMode _attackMode = AttackMode.ATK_AUTO;

    //필요 속성
    [Header("총알 프리펩")]
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private GameObject _miniBulletPrefab;

    [Header("총구")]
    [SerializeField] private Transform _firePositionL;
    [SerializeField] private Transform _firePositionR;
    [SerializeField] private Transform _firePositionSubL;
    [SerializeField] private Transform _firePositionSubR;

    [Header("연사율")]
    [SerializeField] private float _fireRate = 0.6f;

   


    private float cooldownTime = 0f;
   

    private void Update()
    {
       FireCoolTimer();
       SwitchAtkMode();


        if (cooldownTime >= _fireRate)
        { 
            Fire();
        }
       
    }

    private void Fire()
    {

        if (Input.GetKey(KeyCode.Space) || _attackMode==AttackMode.ATK_AUTO)
        {
            MakeBullets();
            cooldownTime = 0f;
        }
    }
   

    private void MakeBullets()
    {
        //클래스 -> 객체(속성+기능) -> 메모리에 실제로 로드된 객체를 인스턴스라고 한다.
        GameObject bulletObj_1=Instantiate(_bulletPrefab, _firePositionL.position, Quaternion.identity);
        GameObject bulletObj_2 = Instantiate(_bulletPrefab, _firePositionR.position, Quaternion.identity);

        GameObject miniBulletObj_1 = Instantiate(_miniBulletPrefab, _firePositionSubL.position, Quaternion.identity);
        GameObject miniBulletObj_2 = Instantiate(_miniBulletPrefab, _firePositionSubR.position, Quaternion.identity);

    }

    private void SwitchAtkMode()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
            _attackMode= AttackMode.ATK_AUTO;

        if(Input.GetKeyDown(KeyCode.Alpha2))
            _attackMode=AttackMode.ATK_MANUAL;
    }
    private void FireCoolTimer()
    {
        cooldownTime += Time.deltaTime;
    }
}
