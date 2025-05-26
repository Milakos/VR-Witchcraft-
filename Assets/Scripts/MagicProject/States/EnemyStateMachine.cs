using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateMachine : MonoBehaviour
{
    [Header("Attributes")]
    private IEnemyState currentState;
    public Animator animator;
    public NavMeshAgent _agent;
    public EnemyBase enemy;
    public PatrolRoute _patrolRoute;
    public FieldOfView _fov;
    internal Transform _currentPoint;
    
    [Header("Timers")]
    public float _threshold = 0.5f;
    public float _stunnedTime = 3f;
    
    public float _waitTimer = 2f;
    public float waitTimer = 2.5f;
    public float waitTime = 1.5f;
    
    [Header("Misc")]
    internal Vector3 _investigationPoint;
    internal int _routeIndex = 0;
    
    [Header("Checks")]
    public bool idle = false;
    public bool patrol = false;
    public bool chase = false;
    public bool investigate = false;
    public bool attacking = false;
    
    public bool _moving = false;
    public bool _forwardsAlongPath = true;
    public bool _playerFound = false;
    public bool _waiting = false;

    private void Start()
    {
        _currentPoint = _patrolRoute.route[_routeIndex];
    }
    public void ChangeState(IEnemyState newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState.Enter(this);
    }
    public void Update() => currentState?.Execute();
    
    public void SetInvestigationPoint(Vector3 investigatePoint)
    {
        _investigationPoint = investigatePoint;
        _agent.SetDestination(_investigationPoint);
    }
    public void PlayerFound(Vector3 investigatePoint)
    {
        if (_playerFound) return;
        
        SetInvestigationPoint(investigatePoint);
        enemy.stateMachine.ChangeState(new InvastigateEnemyState());
        // onPlayerFound.Invoke(_fov.creature.head);

        _playerFound = true;
    }
}