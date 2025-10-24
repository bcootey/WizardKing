using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RightHandBaseState : MonoBehaviour
{
    protected RightHandStateManager stateManager;
    public RightHandBaseState(RightHandStateManager stateManager)
    {
        this.stateManager = stateManager;
    }
    public abstract void EnterState();
    public abstract void ExitState();
    public abstract void UpdateState();
}
