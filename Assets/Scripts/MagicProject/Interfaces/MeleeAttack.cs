using UnityEngine;

public class MeleeAttack : IAttackStrategy
{
    public float damage { get; set; }

    public void Attack(EnemyBase enemy, EnemyData data)
    {
        damage = data.rangeDamage;
        enemy.currentMana -= data.manaReduceRate;
        // enemy.particleSystem.Play();
        Debug.Log($"{enemy.name} performs a melee attack!");      
    }
}