using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftHandIdleState : LeftHandBaseState
{
    public LeftHandIdleState(LeftHandStateManager manager) : base(manager) { }
    public override void EnterState()
    {
        stateManager.leftHandAnim.ResetTrigger("Ultimate");
    }
    public override void UpdateState()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (stateManager.mana.CanUseSpell(stateManager.currentSpell))
            {
                stateManager.mana.DecreaseMana(stateManager.currentSpell.mana);
                stateManager.SetNextState(new LeftHandCastingState(stateManager));
                Debug.Log("Test Spam");
            }
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (stateManager.mana.CanUseSpell(stateManager.currentUltimateSpell))
            {
                stateManager.mana.DecreaseMana(stateManager.currentUltimateSpell.mana);
                stateManager.SetNextState(new LeftHandUltimateState(stateManager));
            }
        }
    }
    public override void ExitState()
    {

    }
}
