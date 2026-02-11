using UnityEngine;

public class DragonKnightPrimaryAttackState : DragonKnightBaseState
{
    public DragonKnightPrimaryAttackState(DragonKnightStateManager manager) : base(manager) { }
    
    public override void EnterState()
    {
        stateManager.knightAnim.SetTrigger("PrimaryAttack");
        stateManager.knightAgent.updateRotation = true;
        
        float rand = Random.Range(0, 2);
        if (rand == 0)
        {
            stateManager.knightAnim.SetTrigger("SecondDash");
        }
        float rand2 = Random.Range(0, 2);
        if (rand2 == 0)
        {
            stateManager.knightAnim.SetTrigger("ThirdDash");
        }
    }
    public override void UpdateState()
    {
        if (stateManager.IsParried)
        {
            stateManager.SetNextState(new DragonKnightStaggerState(stateManager));
        }
        
        if (stateManager.firstPrimaryAttackDone == false)
        {
            stateManager.knightAgent.SetDestination(stateManager.playerStats.playerLocation.position);
        }
        else
        {
            stateManager.knightAgent.updateRotation = false;
            stateManager.knightAgent.SetDestination(stateManager.GetPositionInFront(15));
        }

    }
    public override void ExitState()
    {
        stateManager.SetObjectLookTrue();
        stateManager.ReturnToDefaultAcceleration();
        stateManager.ReturnToDefaultSpeed();
        stateManager.firstPrimaryAttackDone =  false;
        stateManager.knightAgent.updateRotation = false;
    }
}
