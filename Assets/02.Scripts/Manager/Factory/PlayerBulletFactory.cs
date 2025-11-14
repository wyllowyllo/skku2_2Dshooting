using System;
using System.Collections.Generic;
using UnityEngine;


public class PlayerBulletFactory : FactoryBase
{
    private static PlayerBulletFactory _instance = null;
    
    [Header("플레이어 총알 프리펩")] 
    [SerializeField] private GameObject _basicBulletPrefab;
    [SerializeField] private GameObject _subBulletPrefab;
    
 
    private List<GameObject> _basicBulletList;
    private List<GameObject> _subBulletList;
    public static PlayerBulletFactory Instance => _instance;

    private Dictionary<EBulletType, List<GameObject>> _listDictionary;
    private Dictionary<EBulletType, GameObject> _prefabDictionary;
    
    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        _instance = this;



        FieldInit();
        PoolInit();
    }

    
    public GameObject MakeBullet(EBulletType bulletType, Vector3 position)
    {
        GameObject bulletObj = null;

        List<GameObject> targetList = _listDictionary[bulletType];
        GameObject targetPrefab = _prefabDictionary[bulletType];
        
        bulletObj = GetIdleObject(targetPrefab, targetList, position);
        
        return bulletObj;
    }

    
    public GameObject MakeBoom(Vector3 position)
    {
        return null;
    }

    private void FieldInit()
    {
        // 리스트 생성
        _basicBulletList = new List<GameObject>();
        _subBulletList = new List<GameObject>();

        
        // 딕셔너리 생성
        _listDictionary = new Dictionary<EBulletType, List<GameObject>>()
        {
            { EBulletType.Basic, _basicBulletList },
            { EBulletType.Sub, _subBulletList },
        };
        
        _prefabDictionary = new Dictionary<EBulletType, GameObject>()
        {
            { EBulletType.Basic, _basicBulletPrefab},
            { EBulletType.Sub, _subBulletPrefab },
        };
    }

    private void PoolInit()
    {
        // 플레이어 기본 총알 오브젝트 풀 생성
        MakePool(_basicBulletPrefab, _basicBulletList, initPoolSize);
        
        // 플레이어  미니 총알 오브젝트 풀 생성
        MakePool(_subBulletPrefab, _subBulletList, initPoolSize);
        
    }
    
    
}