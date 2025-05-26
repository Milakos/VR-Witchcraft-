public interface IEnemyState
{
    void Enter(EnemyStateMachine context);
    void Execute();
    void Exit();
}