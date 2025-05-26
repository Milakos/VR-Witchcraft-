using UnityEngine;
using UnityEngine.AI;

public class RangedAttack : IAttackStrategy
{
    public void Attack(EnemyBase enemy)
    {
        Debug.Log($"{enemy.name} shoots an arrow!");
    }
}