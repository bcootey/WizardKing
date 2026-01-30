using UnityEngine;

public class SpectreFollowState : SpectreBaseState
{
    public SpectreFollowState(SpectreStateManager manager) : base(manager) { }
    public override void EnterState()
    {
        stateManager.ResetAnimationTriggers();
    }
    public override void UpdateState()
    { 
        //gets distance to target from current location
        Transform player = stateManager.playerStats.playerLocation;

        Vector3 targetPos = player.position;
        if (stateManager.keepHeightOffset) targetPos.y += stateManager.heightOffset;

        Vector3 toTarget = targetPos - stateManager.transform.position;
        float distance = toTarget.magnitude;
        
        // Switch to orbit when close enough
        if (distance <= stateManager.engageDistance)
        {
            stateManager.SetNextState(new SpectreOrbitState(stateManager));
            return;
        }

        if (distance >= stateManager.aggroRange)
        {
            stateManager.SetNextState(new SpectreIdleState(stateManager));
            return;
        }
        
        Vector3 desiredVel = toTarget.normalized * stateManager.chaseSpeed;
        stateManager.rb.linearVelocity = Vector3.MoveTowards(
            stateManager.rb.linearVelocity,
            desiredVel,
            stateManager.accel * Time.fixedDeltaTime
        );
        
        
    }
    public override void ExitState()
    {
        
    }
}
