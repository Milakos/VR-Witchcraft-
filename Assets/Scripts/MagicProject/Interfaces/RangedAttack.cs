using UnityEngine;
using UnityEngine.AI;

public class RangedAttack : IAttackStrategy
{
    
    public void Attack(EnemyData enemy)
    {
        enemy.maxMana -= enemy.manaReduceRate;
        // enemy.particleSystem.Play();
        Debug.Log($"{enemy.name} Cast a Spell and Mana is {enemy.maxMana}");       
    }
}