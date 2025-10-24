using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyHealthBaseState : MonoBehaviour
{
    protected EnemyHealthStateManager stateManager;
    public EnemyHealthBaseState(EnemyHealthStateManager stateManager)
    {
        this.stateManager = stateManager;
    }
    public abstract void EnterState();
    public abstract void ExitState();
    public abstract void UpdateState();
}
