using UnityEngine;

public abstract class DragonKnightBaseState
{
    protected DragonKnightStateManager stateManager;
    public DragonKnightBaseState(DragonKnightStateManager stateManager)
    {
        this.stateManager = stateManager;
    }
    public abstract void EnterState();
    public abstract void ExitState();
    public abstract void UpdateState();
}
