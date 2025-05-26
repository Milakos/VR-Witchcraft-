using System;

public class Magician : EnemyBase
{
    private void Start()
    {
        SetAttackStrategy(new RangedAttack());
    }
    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
    }
    public override void Patrol()
    {
        base.Patrol();
    }
    public override void Attack()
    {
        base.Attack();
    }
}
