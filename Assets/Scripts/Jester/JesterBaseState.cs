using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class JesterBaseState : MonoBehaviour
{
    protected JesterStateManager stateManager;
    public JesterBaseState(JesterStateManager stateManager)
    {
        this.stateManager = stateManager;
    }
    public abstract void EnterState();
    public abstract void ExitState();
    public abstract void UpdateState();
}