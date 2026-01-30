using UnityEngine;

public class JesterIdleState : JesterBaseState
{
    public JesterIdleState(JesterStateManager manager) : base(manager) { }
    public override void EnterState()
    {
        
    }
    public override void UpdateState()
    {
        if (stateManager.followRange.WasEntered())
        {
            stateManager.SetNextState(new JesterFollowState(stateManager));
        }
    }
    public override void ExitState()
    {

    }
}
