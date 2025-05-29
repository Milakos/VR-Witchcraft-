using UnityEngine;

public class StunnedEnemyState : IEnemyState
{
    public EnemyStateMachine _context;
    public void Enter(EnemyStateMachine context)
    {
        Debug.Log("Enter Stunned");
        _context = context;
        _context.hitted = true;
        
        if (_context.enemy.currentHealth >= 60)
        {
            _context.animator.SetBool("Hit", false);
        }
        else
        {
            _context.animator.SetBool("Hit", true);
            context.stunned = true;
        }
        _context.animator.Play("GetHit"); 
    }
    public void Execute()
    {
        _context.transform.LookAt(_context.enemy.target.transform);
    }
    public void Exit()
    {
        _context.hitted = false;
        _context.stunned = false;
    }
}