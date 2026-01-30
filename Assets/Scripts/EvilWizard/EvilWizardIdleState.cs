using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilWizardIdleState : EvilWizardBaseState
{
    public EvilWizardIdleState(EvilWizardStateManager manager) : base(manager) { }
    public override void EnterState()
    {
        stateManager.wizardAnim.SetBool("Walk", false);
        stateManager.wizardNav.ResetPath();
    }
    public override void UpdateState()
    {
        stateManager.spellCooldownTimer += Time.deltaTime;
        if (stateManager.spellCooldownTimer >= stateManager.spellCooldownTime)
        {
            stateManager.spellCooldownTimer = 0;
            if (stateManager.spellInRange.IsTargetInRange())
            {
                stateManager.SetNextState(new EvilWizardCastingState(stateManager));
            }
        }
        if (stateManager.meleeInRange.WasEntered())
        {
            stateManager.SetNextState(new EvilWizardMeleeState(stateManager));
        }
    }
    public override void ExitState()
    {

    }
}
