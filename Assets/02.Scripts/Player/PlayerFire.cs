using System;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    //목표 : 스페이스바를 누르면 총알을 만들어서 발사하고 싶다


    //필요 속성
    [Header("총알 프리펩")]
    [SerializeField] private GameObject bulletPrefab;

    [Header("총구")]
    [SerializeField] private Transform firePosition;

    private void Update()
    {
        //1.발사 버튼을 누르고 있으면
        if(Input.GetKey(KeyCode.Space))
        {
            //2. 프리펩으로부터 게임 오브젝트를 생성한다

            Fire();
        }
       

    }

    private void Fire()
    {
        //클래스 -> 객체(속성+기능) -> 메모리에 실제로 로드된 객체를 인스턴스라고 한다.
        GameObject bulletObj=Instantiate(bulletPrefab, firePosition.position, Quaternion.identity);

    }
}
