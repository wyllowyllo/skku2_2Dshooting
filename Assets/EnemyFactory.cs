using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
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
        
        enemyObj = GetIdleEnemy(targetPrefab, targetList, position);

        return enemyObj;
    }
    
    private GameObject GetIdleEnemy(GameObject targetPrefab, List<GameObject> targetList, Vector3 position)
    {
        GameObject enemyObj = null;
        
        for (int i = 0; i < targetList.Count; i++)
        {
            if (targetList[i].activeInHierarchy) continue;
            
            //Debug.Log(_bulletList[i].name);
            enemyObj = targetList[i];
            enemyObj.transform.position = position;
            enemyObj.SetActive(true);
          
            return enemyObj;
        }

        
        //풀이 부족한 경우
        int increment = Mathf.Max((int)(targetList.Count * _poolScaleFactor), 1);
        
        enemyObj = MakePool(targetPrefab, targetList, increment);
        enemyObj.transform.position = position;
        enemyObj.SetActive(true);
      
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
    
    
    private GameObject MakePool(GameObject targetPrefab, List<GameObject> targetList, int count)
    {
        
        for (int i = 0; i < count; i++)
        {
            GameObject enemyObj = Instantiate(targetPrefab, transform);
            enemyObj.SetActive(false);
            targetList.Add(enemyObj);
        }
        
        int lastIndex = targetList.Count - 1;
        return targetList[lastIndex]; //풀의 마지막 인자 반환
    }
}
