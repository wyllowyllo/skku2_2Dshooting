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
    
    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        _instance = this;

        _straightEnemyList = new List<GameObject>();
        _chasingEnemyList = new List<GameObject>();
        
        PoolInit();
        
    }

    public GameObject MakeEnemy(EEnemyType enemyType, Vector3 position)
    {
        GameObject enemyObj = null;

        switch (enemyType)
        {
            case EEnemyType.Straight:
                enemyObj = GetIdleEnemy(_straightEnemyPrefab, _straightEnemyList, position);
                break;
            case EEnemyType.Trace:
                enemyObj = GetIdleEnemy(_chasingEnemyPrefab, _chasingEnemyList, position);
                break;
            
            
            
            default:
                enemyObj = GetIdleEnemy(_straightEnemyPrefab, _straightEnemyList, position);
                break;
        }

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
