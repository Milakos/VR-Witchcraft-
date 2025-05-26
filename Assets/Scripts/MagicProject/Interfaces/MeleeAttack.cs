using UnityEngine;

public class MeleeAttack : IAttackStrategy
{
    public void Attack(EnemyBase enemy)
    {
        Debug.Log($"{enemy.name} performs a melee attack!");
    }
}