using System;
using FullOpaqueVFX;
using UnityEngine;
using UnityEngine.Assertions;

public class Magician : EnemyBase
{
    private EnemyStateMachine _stateMachine;
    
    protected override void Awake()
    {
        _stateMachine = GetComponent<EnemyStateMachine>();
        base.Awake();
    }

    private void Start()
    {
        SetAttackStrategy(new RangedAttack());
    }
    public override void TakeDamage(float damage, Transform _transfrom)
    {
        base.TakeDamage(damage, _transfrom);
    }
    public override void Patrol()
    {
        base.Patrol();
    }
    public override void Attack()
    {
        if (stateMachine._fov.visibleObjects.Count != 0)
        {
            // target.gameObject.GetComponent<EnemyStateMachine>()._onHitted?.Invoke(enemyData.rangeDamage, transform);
            Debug.LogWarning(transform.name + "in Attack");
        }
        base.Attack();
    }
    public void ReturnToIdle()
    {
        stateMachine.animator.SetFloat("Speed", 0f);
        stateMachine.ChangeState(new AttackEnemyState());
    }

    public override void EnableVFX()
    {
       base.EnableVFX();
    }
    public override void OnDeath()
    {
        base.OnDeath();
    }
}
