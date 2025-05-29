using UnityEngine;
using UnityEngine.AI;

public class RangedAttack : IAttackStrategy
{
    public void Attack(EnemyBase enemy, EnemyData data)
    {
        enemy.currentMana -= data.manaReduceRate;
        // enemy.particleSystem.Play();
        Debug.Log($"{enemy.name} Cast a Spell and Mana is {enemy.currentMana}");       
    }
}