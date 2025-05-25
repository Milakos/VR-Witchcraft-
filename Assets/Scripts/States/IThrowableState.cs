using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IThrowableState 
{
    void Enter(ThrowableItem context);
    void FixedUpdate();
    void Exit();
}
