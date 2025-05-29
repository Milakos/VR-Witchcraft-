using System.Collections;
using System.Collections.Generic;
using FullOpaqueVFX;
using UnityEngine;
using UnityEngine.Assertions;

public class Doctor : EnemyBase
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
    public override void TakeDamage(float damage, Transform _transform)
    {
        base.TakeDamage(damage, _transform);
    }
    public override void Patrol()
    {
        base.Patrol();
    }
    public override void Attack()
    {
        if (stateMachine._fov.visibleObjects.Count != 0)
        {
            target.gameObject.GetComponent<EnemyStateMachine>()._onHitted
                ?.Invoke(enemyData.rangeDamage, this.transform);
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
