using UnityEngine;

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