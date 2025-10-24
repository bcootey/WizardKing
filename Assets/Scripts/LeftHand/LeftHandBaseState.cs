using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LeftHandBaseState : MonoBehaviour
{
    protected LeftHandStateManager stateManager;
    public LeftHandBaseState(LeftHandStateManager stateManager)
    {
        this.stateManager = stateManager;
    }
    public abstract void EnterState();
    public abstract void ExitState();
    public abstract void UpdateState();
}
