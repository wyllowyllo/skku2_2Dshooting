using System;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    [SerializeField] private float _scanRange;
    [SerializeField] private LayerMask _targetLayerMask;

    private RaycastHit2D[] _targets;
    public Transform NeareastTargetTr { get; private set; }

    private void Update()
    {
       Scan();
    }

    private void Scan()
    {
        _targets = Physics2D.CircleCastAll(transform.position, _scanRange, Vector2.zero, _targetLayerMask);
        NeareastTargetTr = GetNearest();
    }
    private Transform GetNearest()
    {
        Transform result = null;
        float diff = _scanRange+10f;

        foreach(RaycastHit2D target in _targets)
        {
            if (!target.transform.CompareTag("Enemy"))  continue;
               
            
            Vector3 myPos = transform.position;
            Vector3 targetPos = target.transform.position;
            
            if(myPos.y >=targetPos.y) continue;
                
            float curDiff = Vector3.Distance(myPos, targetPos);

            if (curDiff < diff)
            {
                diff = curDiff;
                result = target.transform;
            }
        }

        return result;
    }
    
    void OnDrawGizmos()
    {
        // CircleCast를 시각적으로 표시
        Gizmos.color = Color.yellow;

        // 시작 지점
        Gizmos.DrawWireSphere(transform.position, _scanRange);

        /*// 끝 지점
        Vector2 endPoint = origin + Vector2.right.normalized * distance;
        Gizmos.DrawWireSphere(endPoint, radius);

        // 선 연결
        Gizmos.DrawLine(transform.position, endPoint);*/
    }
}