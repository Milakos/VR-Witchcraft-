using System;
using UnityEngine;

public class InvastigateEnemyState : IEnemyState
{
    public EnemyStateMachine _context;
    public Action onAttack; 
    public void Enter(EnemyStateMachine context)
    {
        Debug.Log("Enter Investigate");
        _context = context;
        context.investigate = true;
        // context.animator.SetFloat("Speed", 1.0f);
        context.animator.SetFloat("Speed", context._agent.velocity.magnitude);
    }
    public void Execute()
    {
        UpdateInvestigate();
    }
    public void Exit()
    {
        _context.investigate = false;
    }
    private void UpdateInvestigate()
    {
        //to do Check to change to Chase and from Chase the Attack
        if (_context.IsPlayerVisible())
        {
            _context.SetInvestigationPoint(_context._fov.visibleObjects[0].position);
            
            if (Vector3.Distance(_context.transform.position, _context._fov.visibleObjects[0].position) < 10.0f)
            {
                _context._agent.isStopped = true;
                _context.ChangeState(new AttackEnemyState());
            }
        }
        else
        {
            
            if (Vector3.Distance(_context.transform.position, _context._investigationPoint) < _context._threshold)
            {
                _context.ReturnToPatrol();
            } 
            _context._playerFound = false;
        }
    }
}