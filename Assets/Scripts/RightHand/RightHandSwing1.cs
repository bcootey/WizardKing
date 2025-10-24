using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightHandSwing1 : RightHandBaseState
{
    public RightHandSwing1(RightHandStateManager manager) : base(manager) { }
    public override void EnterState()
    {
        stateManager.rightHandAnim.SetTrigger("Swing");
        //code to get attack value and normalize it
        stateManager.currentAttackDamage = stateManager.playerStats.baseDamage * 1f;
        stateManager.currentAttackDamageNormalized = Mathf.RoundToInt(stateManager.currentAttackDamage);
        stateManager.UpdateAttackDamage(stateManager.currentAttackDamageNormalized);
    }
    public override void UpdateState()
    {
        if (Input.GetMouseButtonDown(0) && stateManager.swinging == true)
        {
            stateManager.SetNextState(new RightHandSwing2(stateManager));
        }
    }
    public override void ExitState()
    {

    }
}
