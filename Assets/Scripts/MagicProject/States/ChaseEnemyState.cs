using UnityEngine;

public class ChaseEnemyState : IEnemyState
{
    public EnemyStateMachine _context;
    public void Enter(EnemyStateMachine context)
    {
        Debug.Log("Enter Run");
        context.chase = true;
        context.animator.SetFloat("Speed", 1.0f);
    }
    public void Execute()
    {
        
    }
    public void Exit()
    {
        _context.chase = false;
    }
}