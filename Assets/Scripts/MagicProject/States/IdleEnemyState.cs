using UnityEngine;

public class IdleEnemyState : IEnemyState
{
    private EnemyStateMachine _context;
    public void Enter(EnemyStateMachine context)
    {
        Debug.Log("Enter Idle");
        _context = context;
        _context.idle = true;
        _context.animator.SetFloat("Speed", 0.0f);
    }
    public void Execute()
    {
        if (_context._fov.visibleObjects.Count > 0)
        {
            _context.PlayerFound(_context._fov.visibleObjects[0].position);
        }
        if (!_context._waiting)
        {
            _context.enemy.stateMachine.ChangeState(new PatrolEnemyState());
        }
    }
    public void Exit()
    {
        _context.idle = false;
    }

}