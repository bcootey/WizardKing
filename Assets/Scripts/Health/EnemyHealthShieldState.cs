using UnityEngine;

public class EnemyHealthShieldState : EnemyHealthBaseState
{
    public EnemyHealthShieldState(EnemyHealthStateManager manager) : base(manager) { }
    public override void EnterState()
    {
        Debug.Log("Shield State");
        stateManager.shieldState = EnemyHealthStateManager.EnemyShieldState.Shield;
        stateManager.shieldObject.SetActive(true);
    }
    public override void UpdateState()
    {
        if (stateManager.shieldHealth <= 0)
        {
            stateManager.shieldObject.SetActive(false);
            Instantiate(stateManager.shieldBreakParticles, stateManager.goreSpawnPoint.position, stateManager.goreSpawnPoint.rotation);
            stateManager.SetNextState(new EnemyHealthNeutralState(stateManager));
        }
    }
    public override void ExitState()
    {

    }
}
