using UnityEngine;

public class StunnedEnemyState : IEnemyState
{
    public EnemyStateMachine _context;
    public void Enter(EnemyStateMachine context)
    {
        Debug.Log("Enter Stunned");
        _context = context;
        
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
        
    }
    public void Exit()
    {
        _context.stunned = false;
    }
}
public class DeadEnemyState : IEnemyState
{
    public EnemyStateMachine _context;
    public void Enter(EnemyStateMachine context)
    {
        Debug.Log("Enter Dead");
        _context = context;
        _context.animator.Play("Death"); 
        context.dead = true;
    }
    public void Execute()
    {
        
    }
    public void Exit()
    {
        _context.dead = false;
    }
}