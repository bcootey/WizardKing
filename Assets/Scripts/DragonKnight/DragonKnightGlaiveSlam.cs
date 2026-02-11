using UnityEngine;

public class DragonKnightGlaiveSlam : DragonKnightBaseState
{
    public DragonKnightGlaiveSlam(DragonKnightStateManager manager) : base(manager) { }
    public override void EnterState()
    {
        stateManager.knightAnim.SetTrigger("GlaiveSlam");
    }
    public override void UpdateState()
    {
        
    }
    public override void ExitState()
    {
        
    }
}
