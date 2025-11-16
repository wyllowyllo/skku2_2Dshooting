using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 적 생성 풀 담당 팩토리
/// </summary>
public class EnemyFactory : FactoryBase
{
    private static EnemyFactory s_instance = null;
    
    [Header("적 프리펩")] 
    [SerializeField] private GameObject _straightEnemyPrefab;
    [SerializeField] private GameObject _chasingEnemyPrefab;
    [SerializeField] private GameObject _bossPrefab;

    [Header("풀링")]
    [SerializeField] private int _initPoolSize = 30;
    [SerializeField] private float _poolScaleFactor = 0.5f;
    private List<GameObject> _straightEnemyList;
    private List<GameObject> _chasingEnemyList;
    
    public static EnemyFactory Instance => s_instance;
    
    private Dictionary<EEnemyType, List<GameObject>> _listDictionary;
    private Dictionary<EEnemyType, GameObject> _prefabDictionary;

    
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
    /// 적 생성 요청 시 호출
    /// </summary>
    /// <param name="enemyType"> 적 타입 </param>
    /// <param name="position"> 생성 위치 </param>
    /// <returns> 적 오브젝트 반환 </returns>
    public GameObject MakeEnemy(EEnemyType enemyType, Vector3 position)
    {
        GameObject enemyObj = null;

        List<GameObject> targetList = _listDictionary[enemyType];
        GameObject targetPrefab = _prefabDictionary[enemyType];
        
        enemyObj = GetIdleObject(targetPrefab, targetList, position);

        return enemyObj;
    }

    public GameObject MakeBoss(Vector3 position)
    {
        return  Instantiate(_bossPrefab, position, Quaternion.identity, transform);
    }
    
   
    
    private void FieldInit()
    {
        // 리스트 생성
        _straightEnemyList = new List<GameObject>();
        _chasingEnemyList = new List<GameObject>();

        
        // 딕셔너리 생성
        _listDictionary = new Dictionary<EEnemyType, List<GameObject>>()
        {
            { EEnemyType.Straight, _straightEnemyList },
            { EEnemyType.Trace, _chasingEnemyList },
        };
        
        _prefabDictionary = new Dictionary<EEnemyType, GameObject>()
        {
            { EEnemyType.Straight, _straightEnemyPrefab },
            { EEnemyType.Trace, _chasingEnemyPrefab },
        };
    }
    private void PoolInit()
    {
        // 플레이어 기본 총알 오브젝트 풀 생성
        MakePool(_straightEnemyPrefab, _straightEnemyList, _initPoolSize);
        
        // 플레이어  미니 총알 오브젝트 풀 생성
        MakePool(_chasingEnemyPrefab, _chasingEnemyList, _initPoolSize);
    }
    
    
   
    
}
