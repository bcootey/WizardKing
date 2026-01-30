using UnityEngine;

public class JesterStunState : JesterBaseState
{
    public JesterStunState(JesterStateManager manager) : base(manager) { }
    public override void EnterState()
    {
        stateManager.ResetAnimations();
        stateManager.jesterAnim.SetTrigger("Stagger");
        stateManager.lookAtPlayer.isLooking = false;
        stateManager.jesterNav.speed = 0;
        stateManager.jesterNav.Stop();
    }
    public override void UpdateState()
    {
        
    }
    public override void ExitState()
    {
        stateManager.IsParried = false;
        stateManager.lookAtPlayer.isLooking = true;
        stateManager.jesterNav.speed = stateManager.defaultSpeed;
        Debug.Log("JesterStunState ExitState");
    }
}
