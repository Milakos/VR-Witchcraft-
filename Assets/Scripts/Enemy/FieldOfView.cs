using System;
using System.Collections;
using System.Collections.Generic;
using FullOpaqueVFX;
using Unity.XR.CoreUtils;
using UnityEditor;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public List<Transform> visibleObjects;
    public Creature enemy;
    
    [SerializeField] private Color _gizmoColor = Color.red;
    [SerializeField] private float _viewRadius = 6f;
    [SerializeField] private float _viewAngle = 30f;
    [SerializeField] private LayerMask _blockingLayers;
    
    [SerializeField] private VFX_SpellManager _spellManager;

    // Update is called once per frame
    void Update()
    {
        // if (enemy.stateMachine.hitted) return;
        
        visibleObjects.Clear();
        
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, _viewRadius);
        foreach (Collider target in targetsInViewRadius)
        {
            if (!target.TryGetComponent(out Creature targetCreature))
            {
                // targetCreature = target.GetComponent<XROrigin>();
                if(!targetCreature) continue;
            }
            
            if (enemy.team == targetCreature.team) continue;

            Vector3 directionToTarget = (target.transform.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < _viewAngle)
            {
                Vector3 headPos = enemy.head.position;
                Vector3 targetHeadPos = targetCreature.head.position;

                Vector3 dirToTargetHead = (targetHeadPos - headPos).normalized;
                float distToTargetHead = Vector3.Distance(headPos, targetHeadPos);

                if (Physics.Raycast(headPos, dirToTargetHead, distToTargetHead, _blockingLayers))
                {
                    continue;
                }
                
                Debug.DrawLine(headPos,targetHeadPos, Color.green);
                
                visibleObjects.Add(targetCreature.gameObject.transform);
                _spellManager.target = target.transform;
                if(target == null)
                    GetComponent<EnemyBase>().target = target.gameObject.transform;
                // enemy.target = target.transform;
            }
        }
    }

    #if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = _gizmoColor;
        Handles.color = _gizmoColor;
        
        Handles.DrawWireArc(transform.position,transform.up,transform.forward, _viewAngle, _viewRadius);
        Handles.DrawWireArc(transform.position,transform.up,transform.forward, -_viewAngle, _viewRadius);

        Vector3 lineA = Quaternion.AngleAxis(_viewAngle, transform.up) * transform.forward;
        Vector3 lineB = Quaternion.AngleAxis(-_viewAngle, transform.up) * transform.forward;
        Handles.DrawLine(transform.position, transform.position + (lineA * _viewRadius));
        Handles.DrawLine(transform.position, transform.position + (lineB * _viewRadius));
    }
    #endif
}
