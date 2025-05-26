using UnityEngine;

public class InvastigateEnemyState : IEnemyState
{
    public EnemyStateMachine _context;
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
        if (_context._fov.visibleObjects.Count > 0)
        {
            _context.SetInvestigationPoint(_context._fov.visibleObjects[0].position);
            
            if (Vector3.Distance(_context.transform.position, _context._fov.visibleObjects[0].position) < 5.0f)
            {
                Debug.Log("Attack");
            }
        }
        // if (Vector3.Distance(_context.transform.position, _context._investigationPoint) < _context._threshold)
        // {
        //     _context._waitTimer += Time.deltaTime;
        //     if (_context._waitTimer > _context.waitTime)
        //     {
        //         ReturnToPatrol();
        //     }
        // }
    }
    private void ReturnToPatrol()
    {
        Debug.Log("Enemy returning to patrol");
        _context.enemy.stateMachine.ChangeState(new PatrolEnemyState());
        _context._waitTimer = 0;
        _context._moving = false;
    }
}