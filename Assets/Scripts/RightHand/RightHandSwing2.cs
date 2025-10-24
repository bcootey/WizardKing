using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightHandSwing2 : RightHandBaseState
{
    public RightHandSwing2(RightHandStateManager manager) : base(manager) { }
    public override void EnterState()
    {
        stateManager.rightHandAnim.SetTrigger("Swing2");
        //code to get attack value and normalize it
        stateManager.currentAttackDamage = stateManager.playerStats.baseDamage * 1.2f;
        stateManager.currentAttackDamageNormalized = Mathf.RoundToInt(stateManager.currentAttackDamage);
        stateManager.UpdateAttackDamage(stateManager.currentAttackDamageNormalized);
    }
    public override void UpdateState()
    {
        if (Input.GetMouseButtonDown(0) && stateManager.swinging == true)
        {
            stateManager.SetNextState(new RightHandSwing3(stateManager));
        }
        
    }
    public override void ExitState()
    {

    }
}
