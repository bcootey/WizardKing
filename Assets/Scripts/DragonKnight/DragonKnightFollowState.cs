using UnityEngine;

public class DragonKnightFollowState : DragonKnightBaseState
{
    public DragonKnightFollowState(DragonKnightStateManager manager) : base(manager) { }
    public override void EnterState()
    {
       stateManager.knightAnim.SetTrigger("Walk");
       stateManager.knightAgent.isStopped = false;
       stateManager.knightAgent.updatePosition = true;
       stateManager.knightAgent.updateRotation = false;
    }
    public override void UpdateState()
    {
        stateManager.knightAgent.SetDestination(stateManager.playerStats.playerLocation.position);
        if (stateManager.playerCombatStateDetector.WasEntered())
        {
            stateManager.SetNextState(new DragonKnightCombatState(stateManager));
        }
    }
    public override void ExitState()
    {
        
    }
}
