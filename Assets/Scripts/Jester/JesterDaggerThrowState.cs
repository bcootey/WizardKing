using UnityEngine;

public class JesterDaggerThrowState : JesterBaseState
{
    public JesterDaggerThrowState(JesterStateManager manager) : base(manager) { }
    public override void EnterState()
    {
        stateManager.jesterAnim.SetTrigger("DaggerThrow");
    }
    public override void UpdateState()
    {
        
    }
    public override void ExitState()
    {

    }
}
