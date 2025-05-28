using System.Collections;
using System.Collections.Generic;
using FullOpaqueVFX;
using UnityEngine;
using UnityEngine.Assertions;

public class Doctor : EnemyBase
{
    private EnemyStateMachine _stateMachine;
    [SerializeField] private VFX_SpellManager spellManager;
    private Transform target;
    protected override void Awake()
    {
        if (enemyData.fighterType == EnemyData.Occupation.Magician)
        {
            Assert.IsNotNull(spellManager); 
        }
        else
        {
            spellManager = GetComponent<VFX_SpellManager>();
        }
        spellManager.currentSpell = enemyData.spellData;
        _stateMachine = GetComponent<EnemyStateMachine>();
        base.Awake();
    }

    private void Start()
    {
        SetAttackStrategy(new RangedAttack());
    }

    private void OnEnable()
    {
        // _stateMachine._onAttack += Attack;
        
    }

    private void OnDisable()
    {
        // _stateMachine._onAttack -= Attack;
        
    }

    private void Update()
    {
        if (_stateMachine._fov.visibleObjects.Count != 0)
        {
            target = _stateMachine._fov.visibleObjects[0].gameObject.transform;
        }
        else
        {
            target = null;
        }
        if (target != null)
        {
            transform.LookAt(target.position, Vector3.up);
        }

        // spellManager.mockTrigger = enemyData.play;
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
        spellManager.target = _stateMachine._fov.visibleObjects[0].gameObject.transform;
        target.gameObject.GetComponent<EnemyStateMachine>()._onHitted?.Invoke(10f);
        base.Attack();
    }

    public void ReturnToIdle()
    {
        stateMachine.ChangeState(new IdleEnemyState());
    }
    public override void EnableVFX()
    {
        base.EnableVFX();
    }
}
