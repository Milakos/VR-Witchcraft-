using UnityEngine;

public class IdleEnemyState : IEnemyState
{
    private EnemyStateMachine _context;
    public void Enter(EnemyStateMachine context)
    {
        Debug.Log("Enter Idle");
        _context = context;
        _context.idle = true;
        _context._moving = false;
        // _context._waiting = true;
        _context.animator.SetFloat("Speed", 0.0f);
        _context.StartCoroutine(_context.WaitAtWaypoint());
    }
    public void Execute()
    {
        
        _context.coolDownTimer -= Time.deltaTime; 
        
        if (_context.coolDownTimer <= 0)
        {
            if (_context.IsPlayerVisible())
            {
                _context.PlayerFound(_context._fov.visibleObjects[0].position);
            }
            else
            {
                _context._playerFound = false;
            }

            if (!_context._waiting)
            {
                if(!_context._playerFound)
                    _context.enemy.stateMachine.ChangeState(new PatrolEnemyState());
            }   
        }
    }
    public void Exit()
    {
        _context.idle = false;
        _context.coolDownTimer = 2.0f;
    }

}