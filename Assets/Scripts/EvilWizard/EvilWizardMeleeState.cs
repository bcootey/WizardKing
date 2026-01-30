using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilWizardMeleeState : EvilWizardBaseState
{
    public EvilWizardMeleeState(EvilWizardStateManager manager) : base(manager) { }
    public override void EnterState()
    {
        stateManager.wizardAnim.SetBool("Walk",true);
    }
    public override void UpdateState()
    {
        stateManager.meleeCooldownTimer += Time.deltaTime;
        if (stateManager.meleeCooldownTimer >= stateManager.meleeCooldownTime)
        {
            if(stateManager.swingRange.WasEntered())
            {
                stateManager.meleeCooldownTimer = 0;
                stateManager.wizardAnim.SetTrigger("Attack");
            }
        }
        stateManager.wizardNav.SetDestination(stateManager.playerStats.playerLocation.position);
        if (stateManager.meleeInRange.WasExited())
        {
            stateManager.SetNextState(new EvilWizardIdleState(stateManager));
        }
        if (stateManager.IsParried)
        {
            stateManager.SetNextState(new EvilWizardStunState(stateManager));
        }
    }
    public override void ExitState()
    {
        stateManager.wizardAnim.SetBool("Walk", false);
    }
}
