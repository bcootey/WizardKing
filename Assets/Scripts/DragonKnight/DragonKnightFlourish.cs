using UnityEngine;

public class DragonKnightFlourish : DragonKnightBaseState
{
    public DragonKnightFlourish(DragonKnightStateManager manager) : base(manager) { }
    public override void EnterState()
    {
        stateManager.knightAnim.SetTrigger("Flourish");
        stateManager.knightAgent.ResetPath();
        
        int rand =  Random.Range(0, 2);
        if (rand == 0)
        {
            stateManager.SetNextState(new DragonKnightGlaiveSlam(stateManager));
        }
        if (rand == 1)
        {
            stateManager.SetNextState(new DragonKnightGlaiveSpin(stateManager));
        }
    }
    public override void UpdateState()
    {
        
    }
    public override void ExitState()
    {
        
    }
}
