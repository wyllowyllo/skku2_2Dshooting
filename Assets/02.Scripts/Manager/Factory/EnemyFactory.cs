using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : FactoryBase
{
    private static EnemyFactory _instance = null;
    
    [Header("적 프리펩")] 
    [SerializeField] private GameObject _straightEnemyPrefab;
    [SerializeField] private GameObject _chasingEnemyPrefab;
    
    [Header("풀링")]
    [SerializeField] private int _initPoolSize = 30;
    [SerializeField] private float _poolScaleFactor = 0.5f;
    private List<GameObject> _straightEnemyList;
    private List<GameObject> _chasingEnemyList;
    
    public static EnemyFactory Instance => _instance;
    
    private Dictionary<EEnemyType, List<GameObject>> _listDictionary;
    private Dictionary<EEnemyType, GameObject> _prefabDictionary;

    
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

    public GameObject MakeEnemy(EEnemyType enemyType, Vector3 position)
    {
        GameObject enemyObj = null;

        List<GameObject> targetList = _listDictionary[enemyType];
        GameObject targetPrefab = _prefabDictionary[enemyType];
        
        enemyObj = GetIdleObject(targetPrefab, targetList, position);

        return enemyObj;
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
