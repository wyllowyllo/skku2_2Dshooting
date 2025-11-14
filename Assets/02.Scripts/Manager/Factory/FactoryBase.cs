using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class FactoryBase : MonoBehaviour
{
    [Header("풀 설정 변수")]
    [SerializeField] protected int initPoolSize;
    [SerializeField] protected float poolScaleFactor;

    protected GameObject MakePool(GameObject targetPrefab, List<GameObject> targetList, int count)
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

    protected GameObject GetIdleObject(GameObject targetPrefab, List<GameObject> targetList, Vector3 position)
    {
        GameObject idleObj = null;

        for (int i = 0; i < targetList.Count; i++)
        {
            if (targetList[i].activeInHierarchy) continue;

            idleObj = targetList[i];
            idleObj.transform.position = position;
            idleObj.SetActive(true);

            return idleObj;
        }
        
        //풀이 부족한 경우
        int increment = Mathf.Max((int)(targetList.Count * poolScaleFactor), 1);
        
        idleObj = MakePool(targetPrefab, targetList, increment);
        idleObj.transform.position = position;
        idleObj.SetActive(true);
      
        return idleObj;
    }
}