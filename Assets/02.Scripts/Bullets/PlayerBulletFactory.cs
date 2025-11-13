using System;
using System.Collections.Generic;
using UnityEngine;


public class PlayerBulletFactory : MonoBehaviour
{
    private static PlayerBulletFactory _instance = null;
    
    [Header("플레이어 총알 프리펩")] 
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private GameObject _subBulletPrefab;
    
    [Header("풀링")]
    [SerializeField] private int _initPoolSize = 30;
    private List<GameObject> _bulletList;
    private List<GameObject> _subBulletList = new List<GameObject>();
    public static PlayerBulletFactory Instance => _instance;
    
    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        _instance = this;

        _bulletList = new List<GameObject>();
        _subBulletList = new List<GameObject>();
        
        PoolInit();
    }
    
    
    public GameObject MakeBullet(Vector3 position)
    {
        GameObject bulletObj = null;
        
        for (int i = 0; i < _bulletList.Count; i++)
        {
            if (_bulletList[i].activeInHierarchy) continue;
            
            //Debug.Log(_bulletList[i].name);
            bulletObj = _bulletList[i];
            bulletObj.transform.position = position;
            bulletObj.SetActive(true);
          
            return bulletObj;
        }

        
        //풀이 부족한 경우
        int increment = _bulletList.Count / 2;
        bulletObj = MakePool(_bulletPrefab, _bulletList, increment);
        bulletObj.transform.position = position;
        bulletObj.SetActive(true);
      
        return bulletObj;
    }

    public GameObject MakeSubBullet(Vector3 position)
    {
        GameObject bulletObj = null;
        
        for (int i = 0; i < _subBulletList.Count; i++)
        {
            if (_subBulletList[i].activeInHierarchy) continue;
            
            bulletObj = _subBulletList[i];
            bulletObj.transform.position = position;
            bulletObj.SetActive(true);
            
            return bulletObj;
        }

        
        //풀이 부족한 경우
        int increment = _subBulletList.Count / 2;
        bulletObj = MakePool(_subBulletPrefab, _subBulletList, increment);
        bulletObj.transform.position = position;
        bulletObj.SetActive(true);
        
        return bulletObj;
    }

    public GameObject MakeBoom(Vector3 position)
    {
        return null;
    }

    private void PoolInit()
    {
        // 플레이어 기본 총알 오브젝트 풀 생성
        MakePool(_bulletPrefab, _bulletList, _initPoolSize);
        
        // 플레이어  미니 총알 오브젝트 풀 생성
        MakePool(_subBulletPrefab, _subBulletList, _initPoolSize);
        
    }

    private GameObject MakePool(GameObject targetPrefab, List<GameObject> targetList, int count)
    {
        
        for (int i = 0; i < count; i++)
        {
            GameObject bulletObj = Instantiate(targetPrefab, transform);
            bulletObj.SetActive(false);
            targetList.Add(bulletObj);
        }
        
        return targetList[0]; //풀의 첫번째 인자 반환
    }

   
}
