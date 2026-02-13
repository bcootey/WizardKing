using UnityEngine;

public class DragonKnightStaggerState : DragonKnightBaseState
{
    public DragonKnightStaggerState(DragonKnightStateManager manager) : base(manager) { }
    public override void EnterState()
    {
        stateManager.knightAnim.SetTrigger("Stagger");
        stateManager.knightAgent.ResetPath();
        stateManager.SetObjectLookFalse();
    }
    public override void UpdateState()
    {
        
    }
    public override void ExitState()
    {
        stateManager.IsParried = false;
        stateManager.SetObjectLookFalse();
        stateManager.knightAgent.isStopped = false;
        stateManager.ReturnToDefaultSpeed();
    }
}
