using System;
using FullOpaqueVFX;
using UnityEngine;

public class AttackEnemyState : IEnemyState
{
    public EnemyStateMachine _context;
    private float animationDuration;
    bool hasFinished = false;
    
    public void Enter(EnemyStateMachine context)
    {
        Debug.Log("Enter Attack");
        hasFinished = false;
        _context = context;
        context.attacking = true;
        // _context.enemy.enemyData.play = true;
        context.animator.SetBool("Attack", true);
        // animationDuration = GetAnimationClipLength("SpellCast", context.animator);
    }
    public void Execute()
    {
        if (!_context.IsPlayerVisible())
        {
            _context._playerFound = false;
            _context._moving = false;
            _context.animator.SetBool("Attack", false);
            _context.animator.SetLayerWeight(1, 0.0f);
            _context._agent.isStopped = true;
            _context.ChangeState(new IdleEnemyState());
        }
        else
        {
            AnimatorStateInfo stateInfo = _context.animator.GetCurrentAnimatorStateInfo(0);
            // NormalizedAnimationPlayThrough(stateInfo);
            if (_context._fov.visibleObjects.Count == 0) return;
            
            if (Vector3.Distance(_context.transform.position, _context._fov.visibleObjects[0].position) > 10f)
            {
                _context._agent.isStopped = false;
                _context.SetInvestigationPoint(_context._fov.visibleObjects[0].position);
                _context.animator.SetLayerWeight(1, 0.5f);
            }
            else
            {
                _context.animator.SetLayerWeight(1, 0.0f);
                _context._agent.isStopped = true;
            }
        }
    }
    public void Exit()
    {
        _context.attacking = false;
        _context.animator.SetLayerWeight(1, 0.0f);
        var spell = _context.GetComponent<VFX_SpellManager>();
        spell.MockTriggerFalse();
        spell.target = null;
        _context.animator.StopPlayback();
    }

}