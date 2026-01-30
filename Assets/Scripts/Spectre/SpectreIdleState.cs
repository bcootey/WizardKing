using UnityEngine;

public class SpectreIdleState : SpectreBaseState
{
    public SpectreIdleState(SpectreStateManager manager) : base(manager) { }
    public override void EnterState()
    {
        if (stateManager.rb)
            stateManager.rb.linearVelocity = Vector3.zero;
    }
    public override void UpdateState()
    {
        Transform player = stateManager.playerStats.playerLocation;

        float distance = Vector3.Distance(
            stateManager.transform.position,
            player.position
        );
        
        if (distance <= stateManager.aggroRange)
        {
            stateManager.SetNextState(new SpectreFollowState(stateManager));
            return;
        }
    }
    public override void ExitState()
    {
        
    }
}
