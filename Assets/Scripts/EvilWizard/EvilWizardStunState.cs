using UnityEngine;

public class EvilWizardStunState : EvilWizardBaseState
{
    public EvilWizardStunState(EvilWizardStateManager manager) : base(manager) { }
    public override void EnterState()
    {
        stateManager.wizardAnim.SetTrigger("Stun");
        stateManager.lookAtPlayer.isLooking = false;
        stateManager.wizardNav.speed = 0;
        stateManager.wizardNav.Stop();
    }
    public override void UpdateState()
    {
        
    }
    public override void ExitState()
    {
        stateManager.IsParried = false;
        stateManager.lookAtPlayer.isLooking = true;
        stateManager.wizardNav.speed = stateManager.defaultSpeed;
    }
}
