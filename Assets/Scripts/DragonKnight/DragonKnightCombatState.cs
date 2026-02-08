using UnityEngine;

public class DragonKnightCombatState : DragonKnightBaseState
{
    public DragonKnightCombatState(DragonKnightStateManager manager) : base(manager) { }
    public override void EnterState()
    {
        stateManager.knightAnim.SetTrigger("Walk");
    }
    public override void UpdateState()
    {
        stateManager.primaryAttackCooldownTimer += Time.deltaTime;
        stateManager.knightAgent.SetDestination(stateManager.playerStats.playerLocation.position);
        if (stateManager.playerMeleeDetector.WasEntered())
        {
            if (stateManager.primaryAttackCooldownTimer >= stateManager.primaryAttackCooldown)
            {
                stateManager.primaryAttackCooldownTimer = 0;
                stateManager.SetNextState(new DragonKnightPrimaryAttackState(stateManager));
            }
        }
    }
    public override void ExitState()
    {
        
    }
}
