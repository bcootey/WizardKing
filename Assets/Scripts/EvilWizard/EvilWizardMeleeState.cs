using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilWizardMeleeState : EvilWizardBaseState
{
    public EvilWizardMeleeState(EvilWizardStateManager manager) : base(manager) { }
    public override void EnterState()
    {
        Debug.Log("Melee State");
        stateManager.wizardAnim.SetBool("Walk",true);
    }
    public override void UpdateState()
    {
        stateManager.wizardNav.SetDestination(stateManager.playerStats.playerLocation.position);
        if (stateManager.meleeInRange.WasExited())
        {
            stateManager.SetNextState(new EvilWizardIdleState(stateManager));
        }
        if(stateManager.swingRange.WasEntered())
        {
            stateManager.wizardAnim.SetTrigger("Attack");
        }
    }
    public override void ExitState()
    {
        stateManager.wizardAnim.SetBool("Walk", false);
    }
}
