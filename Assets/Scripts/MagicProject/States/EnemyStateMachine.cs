using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateMachine : MonoBehaviour
{
    [Header("Attributes")]
    [HideInInspector] public IEnemyState currentState;
    public Animator animator;
    public NavMeshAgent _agent;
    public EnemyBase enemy;
    public PatrolRoute _patrolRoute;
    public FieldOfView _fov;
    internal Transform _currentPoint;
    
    
    [Header("Timers")]
    public float _threshold = 0.5f;
    [SerializeField] private float distanceThreshold = 10.0f;
    [SerializeField] private float waitTimer = 2.5f;
    public float coolDownTimer = 2.0f;
    
    [Header("Misc")]
    internal Vector3 _investigationPoint;
    internal int _routeIndex = 0;
    
    [Header("Checks")]
    public bool idle = false;
    public bool patrol = false;
    public bool chase = false;
    public bool investigate = false;
    public bool attacking = false;
    public bool stunned = false;
    public bool hitted = false;
    public bool dead = false;
    
    public bool _moving = false;
    
    public bool _playerFound = false;
    public bool _waiting = false;
    
    //Actions
    public Action _onAttack;
    public Action<float, Transform> _onHitted;

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
        _agent.speed = 7f;
        _agent.SetDestination(_investigationPoint);
    }
    public void PlayerFound(Vector3 investigatePoint)
    {
        if (Vector3.Distance(transform.position, _fov.visibleObjects[0].position) > 10f)
        {
            if (_playerFound) return;
            SetInvestigationPoint(investigatePoint);
            ChangeState(new InvastigateEnemyState());
            _playerFound = true;  
        }
        else
        {
            _agent.isStopped = true;
            ChangeState(new AttackEnemyState());
        }
    }
    public void ReturnToPatrol()
    {
        Debug.Log("Enemy returning to patrol");
        ChangeState(new IdleEnemyState());
        _playerFound = false;
        // _context._waitTimer = 0;
    }
    public IEnumerator WaitAtWaypoint()
    {
        _waiting = true;
        yield return new WaitForSecondsRealtime(waitTimer);
        _waiting = false;
    }
    public bool IsPlayerVisible() => _fov.visibleObjects.Count > 0;
    public bool IsInAttackRange()
    {
        return Vector3.Distance(transform.position, _fov.visibleObjects[0].position) < distanceThreshold;
    }
}