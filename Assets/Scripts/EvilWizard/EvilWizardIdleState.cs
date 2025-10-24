using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilWizardIdleState : EvilWizardBaseState
{
    public EvilWizardIdleState(EvilWizardStateManager manager) : base(manager) { }
    public override void EnterState()
    {
        Debug.Log("IdleState");
        stateManager.wizardAnim.SetBool("Walk", false);
        stateManager.wizardNav.ResetPath();
    }
    public override void UpdateState()
    {
        if (stateManager.meleeInRange.WasEntered())
        {
            stateManager.SetNextState(new EvilWizardMeleeState(stateManager));
        }
        if (stateManager.spellInRange.IsTargetInRange())
        {
            stateManager.SetNextState(new EvilWizardCastingState(stateManager));
        }
    }
    public override void ExitState()
    {

    }
}
