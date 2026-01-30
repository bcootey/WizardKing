using UnityEngine;
public class JesterBackflipState : JesterBaseState
{
    public JesterBackflipState(JesterStateManager manager) : base(manager) { }
    
    Transform target;

    private Vector3 backwards;
    private float distance;
    private float sideways;
    private Vector3 offset;
    private Vector3 randomPos;
    private Vector3 side;
    
    // Stuck detection
    private float stuckTimer;
    private const float stuckVelocityThreshold = 0.01f;
    private const float stuckTimeThreshold = 0.01f;
    public override void EnterState()
    {
        stateManager.ResetAnimations();
        stateManager.jesterAnim.SetTrigger("Backflip");
        
        target = stateManager.gameObject.transform;
        backwards = -target.forward;                     
        distance = Random.Range(6f, 10f);
        sideways = Random.Range(-2f, 2f);
        side = target.right * sideways;
        offset = backwards * distance;
        randomPos = target.position + backwards * distance + side;    
        stateManager.jesterNav.SetDestination(randomPos);
        stuckTimer = 0f;
    }
    public override void UpdateState()
    {
        var agent = stateManager.jesterNav;

        if (agent.pathPending)
            return;

        bool closeEnough = agent.remainingDistance <= agent.stoppingDistance + 0.1f;

        if (closeEnough)
        {
            stateManager.SetNextState(new JesterDaggerThrowState(stateManager));
            return;
        }

        if (agent.hasPath && agent.velocity.sqrMagnitude < stuckVelocityThreshold)
        {
            stuckTimer += Time.deltaTime;

            if (stuckTimer >= stuckTimeThreshold)
            {

                stateManager.SetNextState(new JesterDaggerThrowState(stateManager));
            }
        }
        else
        {
            stuckTimer = 0f;
        }
    }
    
    public override void ExitState()
    {

    }
}
