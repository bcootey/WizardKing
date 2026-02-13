using UnityEngine;

public class DragonKnightIdleState : DragonKnightBaseState
{
    public DragonKnightIdleState(DragonKnightStateManager manager) : base(manager) { }
    public override void EnterState()
    {
       
    }
    public override void UpdateState()
    {
        if (stateManager.playerDistanceDetector.WasEntered())
        {
            stateManager.SetNextState(new DragonKnightFollowState(stateManager));
        }
    }
    public override void ExitState()
    {
        
    }
}
