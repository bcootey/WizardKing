using UnityEngine;

public class JesterMeleeState : JesterBaseState
{
    public JesterMeleeState(JesterStateManager manager) : base(manager) { }
    public override void EnterState()
    {
        stateManager.ResetAnimations();
        stateManager.jesterAnim.SetTrigger("Melee");
    }
    public override void UpdateState()
    {
        stateManager.jesterNav.SetDestination(stateManager.playerStats.playerLocation.position);
        if (stateManager.IsParried)
        {
            stateManager.SetNextState(new JesterStunState(stateManager));
        }
    }
    public override void ExitState()
    {
        
    }
}
