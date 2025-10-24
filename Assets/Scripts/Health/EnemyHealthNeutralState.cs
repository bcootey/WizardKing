using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthNeutralState : EnemyHealthBaseState
{
    public EnemyHealthNeutralState(EnemyHealthStateManager manager) : base(manager) { }
    public override void EnterState()
    {
        Debug.Log("Neutral State");
        stateManager.shieldState = EnemyHealthStateManager.EnemyShieldState.Neutral;
    }
    public override void UpdateState()
    {
        
    }
    public override void ExitState()
    {

    }
}
