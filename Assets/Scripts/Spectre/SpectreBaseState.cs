using UnityEngine;

public abstract class SpectreBaseState
{
    protected SpectreStateManager stateManager;
    public SpectreBaseState(SpectreStateManager stateManager)
    {
        this.stateManager = stateManager;
    }
    public abstract void EnterState();
    public abstract void ExitState();
    public abstract void UpdateState();
}
