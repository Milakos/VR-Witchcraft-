using System.Collections;
using UnityEngine;

public class PatrolEnemyState : IEnemyState
{
    private EnemyStateMachine _context;
    public void Enter(EnemyStateMachine context)
    {
        _context = context;
        Debug.Log("Patrol");
        _context.patrol = true;
        _context.animator.SetFloat("Speed", 0.5f);
    }
    public void Execute()
    {
        if (_context._fov.visibleObjects.Count > 0)
        {
            _context.PlayerFound(_context._fov.visibleObjects[0].position);
        }
        
        UpdatePatrol();
    }
    public void Exit()
    {
        _context.patrol = false;
    }
    private void UpdatePatrol()
    {
        if (_context._waiting) return;
        
        if (!_context._moving)
        {
            NextPatrolPoint();
            _context._agent.SetDestination(_context._currentPoint.position);
            _context._moving = true;
        }

        if (_context._moving && Vector3.Distance(_context.transform.position, _context._currentPoint.position) < _context._threshold)
        {
            _context._moving = false;
            _context.enemy.stateMachine.ChangeState(new IdleEnemyState());
            _context.StartCoroutine(WaitAtWaypoint());
        }
    }

    private void NextPatrolPoint()
    {
        if (_context._forwardsAlongPath)
        {
            _context._routeIndex++;
        }
        else
        {
            _context._routeIndex--;
        }
        if (_context._routeIndex == _context._patrolRoute.route.Count)
        {
            if (_context._patrolRoute.patrolType == PatrolRoute.PatrolType.Loop)
            {
                _context._routeIndex = 0;
            }
            else
            {
                _context._forwardsAlongPath = false;
                _context._routeIndex -= 2;
            }
        }
        if (_context._routeIndex == 0)
        {
            _context._forwardsAlongPath = true;
        }
        _context._currentPoint = _context._patrolRoute.route[_context._routeIndex];
    }
    private IEnumerator WaitAtWaypoint()
    {
        _context._waiting = true;
        yield return new WaitForSeconds(_context.waitTimer);
        _context._waiting = false;
    }
}