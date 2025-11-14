using System;
using System.Collections.Generic;
using UnityEngine;


public class PlayerBulletFactory : MonoBehaviour
{
    private static PlayerBulletFactory _instance = null;
    
    [Header("플레이어 총알 프리펩")] 
    [SerializeField] private GameObject _basicBulletPrefab;
    [SerializeField] private GameObject _subBulletPrefab;
    
    [Header("풀링")]
    [SerializeField] private int _initPoolSize = 30;
    [SerializeField] private float _poolScaleFactor = 0.5f;
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
        
        bulletObj = GetIdleBullet(targetPrefab, targetList, position);
        
        return bulletObj;
    }

    private GameObject GetIdleBullet(GameObject targetPrefab, List<GameObject> targetList, Vector3 position)
    {
        GameObject bulletObj = null;

        for (int i = 0; i < targetList.Count; i++)
        {
            if (targetList[i].activeInHierarchy) continue;

            bulletObj = targetList[i];
            bulletObj.transform.position = position;
            bulletObj.SetActive(true);

            return bulletObj;
        }
        
        //풀이 부족한 경우
        int increment = Mathf.Max((int)(targetList.Count * _poolScaleFactor), 1);
        
        bulletObj = MakePool(targetPrefab, targetList, increment);
        bulletObj.transform.position = position;
        bulletObj.SetActive(true);
      
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
        MakePool(_basicBulletPrefab, _basicBulletList, _initPoolSize);
        
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
        
        int lastIndex = targetList.Count - 1;
        return targetList[lastIndex]; //풀의 마지막 인자 반환
    }

   
}
