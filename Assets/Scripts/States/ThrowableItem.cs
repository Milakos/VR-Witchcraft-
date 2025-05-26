using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
public class ThrowableItem : MonoBehaviour
{
    private IThrowableState _currentState;
    public IdleState IdleState = new IdleState();
    public HoldState HoldState = new HoldState();
    public ThrowState<Rigidbody> ThrowState = new ThrowState<Rigidbody>();
    public LandedState<Rigidbody> LandedState = new LandedState<Rigidbody>();
    public ReturnState<Transform> ReturnState = new ReturnState<Transform>();
    
    [NonSerialized] public bool isIdle = false;
    [NonSerialized] public bool isHolding = false;
    [NonSerialized] public bool isThrown = false;
    [NonSerialized] public bool isLanded = false;
    [NonSerialized] public bool isReturned = false;
    
    [NonSerialized] public bool isLevitating = false;
    
    private void FixedUpdate()
    {
        _currentState?.FixedUpdate();
    }
    public void TransitionToState(IThrowableState newState)
    {
        _currentState?.Exit();
        _currentState = newState;
        _currentState.Enter(this);
    }
}

public class LevitateState : IThrowableState
{
    private ThrowableItem _context;
    public void Enter(ThrowableItem context)
    {
        _context = context;
        _context.isLevitating = true;
        Debug.Log("Enter LevitateState");
    }

    public void FixedUpdate()
    {
    }

    public void Exit()
    {
        _context.isLevitating = false;
    }
}

public class IdleState : IThrowableState
{
    private ThrowableItem _context;
    public void Enter(ThrowableItem context)
    {
        _context = context;
        _context.isIdle = true;
        Debug.Log("Enter IdleState");
    }
    public void FixedUpdate() { }

    public void Exit()
    {
        _context.isIdle = false;
    }
}
public class HoldState : IThrowableState
{
    private ThrowableItem _context;
    public void Enter(ThrowableItem context)
    {
        _context = context;
        _context.isHolding = true;
        Debug.Log("Enter HoldState");
    }

    public void FixedUpdate()
    {
        
    }

    public void Exit()
    {
        _context.isHolding = false;
    }
}
public class ThrowState<TRigid> : IThrowableState where TRigid : Rigidbody 
{
    public ThrowState() { }
    private TRigid _rb;
    private float _torque;
    private IThrowableState _throwableStateImplementation;
    private ThrowableItem _context;
    private EnemyStateMachine enemy;
    public ThrowState(TRigid rb, float torque)
    {
        _rb = rb;
        _torque = torque;
    }
    public void Enter(ThrowableItem context)
    {
        _context = context;
        _context.isThrown = true;
        _rb.isKinematic = false;
        _rb.useGravity = true;
        Debug.Log("Enter ThrowState");
    }
    
    public void FixedUpdate()
    {
        ImplementTorque(_rb);
    }
    public void Exit()
    {
        _context.isThrown = false;
    }
    public void ImplementTorque(Rigidbody rb)
    {
        // rb.AddForce(Vector3.up * _torque, ForceMode.VelocityChange);
        rb.AddTorque(Vector3.up * _torque);
    }
}
public class LandedState<TRigid> : IThrowableState where TRigid : Rigidbody 
{
    private TRigid _rb;
    public LandedState() { }
    public LandedState(TRigid rb)
    {
        _rb = rb;
    }
    private ThrowableItem _context;
    public void Enter(ThrowableItem context)
    {
        Debug.Log("Enter LandedState");
        _context = context;
        InitLand(true);
        _context.isLanded = true;
        
    }

    public void FixedUpdate() { }
    public void Exit()
    {
        _context.isLanded = false;
    }
    private void InitLand(bool value)
    {
        _rb.isKinematic = value;
        _rb.useGravity = !value;
    }

}
public class ReturnState<TGameObj> : IThrowableState where TGameObj : Transform 
{
    private ThrowableItem _context;
    private GameObject obj;
    TGameObj _gameObj;
    private float _duration;
    public ReturnState() { }
    public ReturnState(GameObject go, TGameObj trans, float duration)
    {
        obj = go;
        _gameObj = trans;
        _duration = duration;
    }
    public void Enter(ThrowableItem context)
    {
        Debug.Log("Enter ReturnState");
        _context = context;
        _context.isReturned = true;
        MoveToHand(obj, _gameObj, _duration);
    }
    public void FixedUpdate() { }
    public void Exit()
    {
        _context.isReturned = false;
    }
    public IEnumerator MoveToHand(GameObject go, Transform handTransform, float duration)
    {
        Vector3 startPosition = go.transform.position;
        Quaternion startRotation = go.transform.rotation;

        Vector3 endPosition = handTransform.position;
        Quaternion endRotation = handTransform.rotation;

        float elapsed = 0f;

        while (elapsed < duration)
        {
            float t = elapsed / duration;
            go.transform.position = Vector3.Lerp(startPosition, endPosition, t);
            go.transform.rotation = Quaternion.Slerp(startRotation, endRotation, t);
            elapsed += Time.deltaTime;
            yield return null;
        }
        
        go.transform.position = endPosition;
        go.transform.rotation = endRotation;
        
    }
}
