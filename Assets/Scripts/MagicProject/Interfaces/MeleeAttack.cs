using UnityEngine;

public class MeleeAttack : IAttackStrategy
{
    public void Attack(EnemyData enemy)
    {
        Debug.Log($"{enemy.name} performs a melee attack!");
    }
}