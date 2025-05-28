using System;
using FullOpaqueVFX;
using UnityEngine;

public class EnemyBase : MonoBehaviour, IEnemy
{
    public EnemyData enemyData;
    public EnemyStateMachine stateMachine;
    public Transform head;
    private IAttackStrategy attackStrategy;

    public float currentHealth;
    
    protected virtual void Awake()
    {
        currentHealth = enemyData.maxHealth;
        stateMachine.ChangeState(new PatrolEnemyState());
        stateMachine._onHitted += TakeDamage;
    }
    public virtual void Patrol() { }
    protected void SetAttackStrategy(IAttackStrategy strategy)
    {
        attackStrategy = strategy;
    }
    public virtual void Attack()
    {
        if(enemyData.maxMana > 0)   
            PerformAttack();
    }
    private void PerformAttack()
    {
        attackStrategy?.Attack(enemyData);
    }
    public virtual void TakeDamage(float amount)
    {
        if (currentHealth <= 0)
        {
            stateMachine.ChangeState(new DeadEnemyState());
        }
        if (!stateMachine.stunned && !stateMachine.dead)
        {
            stateMachine.ChangeState(new StunnedEnemyState());  
        }

        stateMachine._agent.isStopped = true;
        currentHealth -= amount;
    }

    public virtual void EnableVFX()
    {
        
    }

}
