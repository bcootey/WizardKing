using UnityEngine;

public class DragonKnightGlaiveSpin : DragonKnightBaseState
{
    public DragonKnightGlaiveSpin(DragonKnightStateManager manager) : base(manager) { }
    public override void EnterState()
    {
        stateManager.knightAnim.SetTrigger("GlaiveSpin");
    }
    public override void UpdateState()
    {

    }
    public override void ExitState()
    {
        stateManager.startedGlaiveSpinAttack = false;
    }
}
