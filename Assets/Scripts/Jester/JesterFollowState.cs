using UnityEngine;

public class JesterFollowState : JesterBaseState
{
    public JesterFollowState(JesterStateManager manager) : base(manager) { }
    public override void EnterState()
    {
        stateManager.jesterAnim.SetTrigger("Walk");
        stateManager.jesterNav.SetDestination(stateManager.playerStats.playerLocation.position);
        stateManager.jesterNav.isStopped = false;
    }
    public override void UpdateState()
    {
        
        stateManager.jesterNav.SetDestination(stateManager.playerStats.playerLocation.position);
        if (stateManager.followRange.WasExited())
        {
            stateManager.SetNextState(new JesterIdleState(stateManager));
        }
        
        stateManager.meleeCooldownTimer += Time.deltaTime;
        stateManager.backflipCooldownTimer += Time.deltaTime;
        stateManager.spinSlashCooldownTimer += Time.deltaTime;
        if (stateManager.meleeRange.WasEntered())
        {
            if (stateManager.meleeCooldownTimer >= stateManager.meleeCooldown)
            {
                int rand = Random.Range(0, 3);
                if (rand == 0)
                {
                    if (stateManager.backflipCooldownTimer >= stateManager.backflipCooldown)
                    {
                        stateManager.backflipCooldownTimer = 0;
                        stateManager.SetNextState(new JesterBackflipState(stateManager));
                    }
                    return;
                }

                if (rand == 1)
                {
                    stateManager.meleeCooldownTimer = 0;
                    stateManager.SetNextState(new JesterMeleeState(stateManager));
                }

                if (rand == 2)
                {
                    if (stateManager.spinSlashCooldownTimer >= stateManager.spinSlashCooldown)
                    {
                        stateManager.spinSlashCooldownTimer = 0;
                        stateManager.SetNextState(new JesterSpinState(stateManager));
                    }
                    return;
                }
            }
        }
    }
    public override void ExitState()
    {

    }
}
