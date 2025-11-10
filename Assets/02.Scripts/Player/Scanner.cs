using System;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    [SerializeField] private float _scanRange;
    [SerializeField] private LayerMask _targetLayerMask;

    public Transform NeareastTargetTr { get; private set; }
    
    private RaycastHit2D[] _targets;
  

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
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _scanRange);

    }
}