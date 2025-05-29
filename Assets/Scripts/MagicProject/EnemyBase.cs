using FullOpaqueVFX;
using UnityEngine;
using UnityEngine.Assertions;

public class EnemyBase : MonoBehaviour, IEnemy
{
    public EnemyData enemyData;
    public EnemyStateMachine stateMachine;
    public VFX_SpellManager spellManager;
    public Transform target;
    public Transform head;
    private IAttackStrategy attackStrategy;

    public float currentHealth;
    public float currentMana;
    protected virtual void Awake()
    {
        if (enemyData.fighterType == EnemyData.Occupation.Magician)
        {
            Assert.IsNotNull(spellManager); 
        }
        spellManager.currentSpell = enemyData.spellData;
        currentHealth = enemyData.maxHealth;
        currentMana = enemyData.maxMana;
        stateMachine.ChangeState(new PatrolEnemyState());
        stateMachine._onHitted += TakeDamage;
    }
    public virtual void Patrol() { }
    protected void SetAttackStrategy(IAttackStrategy strategy)
    {
        attackStrategy = strategy;
    }
    public virtual void Attack()
    {
        if(enemyData.maxMana > 0)   
            PerformAttack();
    }
    private void PerformAttack()
    {
        attackStrategy?.Attack(this, enemyData);
    }
    public virtual void TakeDamage(float amount, Transform tarns)
    {
        if (currentHealth <= 0)
        {
            stateMachine.ChangeState(new DeadEnemyState());
        }
        if (!stateMachine.stunned && !stateMachine.dead)
        {
            stateMachine.ChangeState(new StunnedEnemyState());  
        }
        stateMachine._agent.isStopped = true;
        currentHealth -= amount;
        
        if (stateMachine._fov.visibleObjects.Count == 0)
        {
            target = tarns;
            // this.transform.LookAt(target);
            Debug.LogWarning("Taken Damage from" + tarns.name);
        }
    }

    public virtual void EnableVFX()
    {
        
    }

    public virtual void OnDeath()
    {
        Destroy(stateMachine._agent.gameObject, 1f);
    }

}
