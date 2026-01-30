using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilWizardCastingState : EvilWizardBaseState
{
    public EvilWizardCastingState(EvilWizardStateManager manager) : base(manager) { }
    public override void EnterState()
    {                             
        stateManager.wizardAnim.SetTrigger("Cast");
        stateManager.ChooseSpell();
    }
    public override void UpdateState()
    {

    }
    public override void ExitState()
    {

    }
}
