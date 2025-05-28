using System;
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
        // _context.enemy.enemyData.play = false;
        _context.animator.StopPlayback();
    }
    // private void NormalizedAnimationPlayThrough(AnimatorStateInfo stateInfo)
    // {
    //     // Check if the animator is playing the desired state
    //     if (stateInfo.IsName("SpellCast"))
    //     {
    //         // normalizedTime goes from 0 to 1 for a single playthrough
    //         if (animationDuration >= stateInfo.normalizedTime  && !hasFinished)
    //         {
    //             hasFinished = true;
    //             _context.enemy.enemyData.play = true;
    //             _context._onAttack?.Invoke();
    //         }
    //     }
    //     else
    //     {
    //         _context.enemy.enemyData.play = false;
    //         hasFinished = false;
    //     }
    // }
    // float GetAnimationClipLength(string clipName, Animator animator)
    // {
    //     RuntimeAnimatorController ac = animator.runtimeAnimatorController;
    //
    //     foreach (var clip in ac.animationClips)
    //     {
    //         if (clip.name == clipName)
    //         {
    //             return clip.length;
    //         }
    //     }
    //
    //     Debug.LogWarning("Animation clip not found!");
    //     return 0f;
    // }
}