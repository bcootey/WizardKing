using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightHandSwing3 : RightHandBaseState
{
    public RightHandSwing3(RightHandStateManager manager) : base(manager) { }
    public override void EnterState()
    {
        stateManager.rightHandAnim.SetTrigger("Swing3");
        //code to get attack value and normalize it
        stateManager.currentAttackDamage = stateManager.playerStats.baseDamage * 1.5f;
        stateManager.currentAttackDamageNormalized = Mathf.RoundToInt(stateManager.currentAttackDamage);
        stateManager.UpdateAttackDamage(stateManager.currentAttackDamageNormalized);
    }
    public override void UpdateState()
    {

    }
    public override void ExitState()
    {

    }
}
