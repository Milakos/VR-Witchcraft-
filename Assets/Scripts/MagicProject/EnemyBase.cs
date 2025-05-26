using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBase : MonoBehaviour, IEnemy
{
    public EnemyData enemyData;
    public EnemyStateMachine stateMachine;
    public Transform head;
    public float currentHealth;
    public float currentMana;
    private IAttackStrategy attackStrategy;
    protected virtual void Awake()
    {
        currentHealth = enemyData.maxHealth;
        currentMana = enemyData.maxMana;
        stateMachine.ChangeState(new PatrolEnemyState());
    }
    public virtual void Patrol() { }
    public virtual void Attack()
    {
        PerformAttack();
    }
    private void PerformAttack()
    {
        attackStrategy?.Attack(this);
    }
    protected void SetAttackStrategy(IAttackStrategy strategy)
    {
        attackStrategy = strategy;
    }
    
    
    public virtual void TakeDamage(float amount)
    {
        currentHealth -= amount;
    }

}
