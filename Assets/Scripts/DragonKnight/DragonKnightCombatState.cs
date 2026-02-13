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
        //cooldowns timers
        stateManager.primaryAttackCooldownTimer += Time.deltaTime;
        stateManager.flourishCooldownTimer += Time.deltaTime;
        stateManager.knightAgent.SetDestination(stateManager.playerStats.playerLocation.position);
        //randomly generates a move and checks if its off cooldown
        if (stateManager.playerMeleeDetector.WasEntered())
        {
            int rand = Random.Range(0, 2);
            if (rand == 0)
            {
                if (stateManager.primaryAttackCooldownTimer >= stateManager.primaryAttackCooldown)
                {
                    stateManager.primaryAttackCooldownTimer = 0;
                    stateManager.SetNextState(new DragonKnightPrimaryAttackState(stateManager));
                }
            }

            if (rand == 1)
            {
                if (stateManager.flourishCooldownTimer >= stateManager.flourishCooldown)
                {
                    stateManager.flourishCooldownTimer = 0;
                    stateManager.SetNextState(new DragonKnightFlourish(stateManager));
                }
            }
        }

        if (stateManager.playerMeleeDetector.WasExited())
        {
            stateManager.SetNextState(new DragonKnightFollowState(stateManager));
        }
    }
    public override void ExitState()
    {
        
    }
}
