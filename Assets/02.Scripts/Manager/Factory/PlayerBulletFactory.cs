using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 플레이어 총알 풀 담당 팩토리
/// </summary>
public class PlayerBulletFactory : FactoryBase
{
    private static PlayerBulletFactory s_instance = null;
    
    [Header("플레이어 총알 프리펩")] 
    [SerializeField] private GameObject _basicBulletPrefab;
    [SerializeField] private GameObject _subBulletPrefab;
    
 
    private List<GameObject> _basicBulletList;
    private List<GameObject> _subBulletList;
    
    /// <summary>
    /// PlayerBulletFactory 접근용 프로퍼티
    /// </summary>
    public static PlayerBulletFactory Instance => s_instance;

    private Dictionary<EBulletType, List<GameObject>> _listDictionary;
    private Dictionary<EBulletType, GameObject> _prefabDictionary;
    
    private void Awake()
    {
        if (s_instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        s_instance = this;



        FieldInit();
        PoolInit();
    }

    /// <summary>
    /// 총알 생성 요청 시 호출
    /// </summary>
    /// <param name="bulletType"> 총알 타입 </param>
    /// <param name="position"> 생성 위치 </param>
    /// <returns> 총알 오브젝트 반환 </returns>
    public GameObject MakeBullet(EBulletType bulletType, Vector3 position)
    {
        GameObject bulletObj = null;

        List<GameObject> targetList = _listDictionary[bulletType];
        GameObject targetPrefab = _prefabDictionary[bulletType];
        
        bulletObj = GetIdleObject(targetPrefab, targetList, position);
        
        return bulletObj;
    }

    /// <summary>
    /// 플레이어 폭탄 생성 요청 시 호출
    /// </summary>
    /// <param name="position"> 생성 위치 </param>
    /// <returns></returns>
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