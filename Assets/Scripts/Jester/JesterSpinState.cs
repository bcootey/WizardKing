using UnityEngine;
using UnityEngine.AI;

public class JesterSpinState : JesterBaseState
{
    public JesterSpinState(JesterStateManager manager) : base(manager) { }

    [Header("Point Picking (NavMesh-safe)")]
    private float minRadius = .25f;
    private float maxRadius = 1.5f;
    private float navmeshSampleRadius = 2.0f;
    private int maxPickTries = 5;
    private bool requireCompletePath = true;

    [Header("Movement")]
    private float arriveDistance = .1f;
    private float arriveTimeout = 2.5f;
    private float repathInterval = 0.1f;

    [Header("Attack")]
    private float spinDuration = 1.0f;
    private float betweenPointsDelay = 0.15f;

    [Header("Arrival Detection")]
    private float velocityArriveThreshold = 0.1f;
    private float directDistanceArrive = 1.0f;
    private float repathGraceTime = 0.15f;

    private enum Phase { PickPoint, MoveToPoint, Spin, Between, Done }
    private Phase phase;

    private int pointsDone;
    private Vector3 currentPoint;

    private float timer;
    private float repathTimer;
    
    private static readonly int WalkHash = Animator.StringToHash("Walk");
    private static readonly int SpinHash = Animator.StringToHash("Spin");
    
    private GameObject telegraphInstance;
    
    private bool lineActive;

    public override void EnterState()
    {
        stateManager.ResetAnimations();
        stateManager.jesterAnim.SetTrigger(WalkHash);
        stateManager.lookAtPlayer.isLooking = false;
        stateManager.jesterNav.updateRotation = true;;
        //change speed
        stateManager.jesterNav.speed = 7.4f;
        

        var agent = stateManager.jesterNav;
        agent.isStopped = false;
        agent.stoppingDistance = .2f;

        pointsDone = 0;
        phase = Phase.PickPoint;

        timer = 0f;
        repathTimer = 0f;
        
        stateManager.lineRenderer.gameObject.SetActive(true);
        stateManager.lineRenderer.useWorldSpace = true;
        lineActive = false;
        HideLine();
    }

    public override void UpdateState()
    {
        if (lineActive)
            UpdateLineEveryFrame();
        switch (phase)
        {
            case Phase.PickPoint:
                PickAndStartMove();
                break;

            case Phase.MoveToPoint:
                TickMoveToPoint();
                break;

            case Phase.Spin:
                TickSpin();
                break;

            case Phase.Between:
                TickBetween();
                break;

            case Phase.Done:
                stateManager.SetNextState(new JesterFollowState(stateManager));
                break;
        }
        
    }

    public override void ExitState()
    {
        stateManager.jesterNav.isStopped = false;
        stateManager.jesterNav.stoppingDistance = stateManager.defaultStoppingDistance;
        //reset jester look
        stateManager.lookAtPlayer.isLooking = true;
        stateManager.jesterNav.updateRotation = false;;
        //reset speed
        stateManager.jesterNav.speed = stateManager.defaultSpeed;
        CleanupTelegraph();
        stateManager.lineRenderer.gameObject.SetActive(false);
    }

    private void PickAndStartMove()
    {
        var agent = stateManager.jesterNav;

        currentPoint = PickValidNavMeshPointNearPlayer();

        SpawnTelegraph(currentPoint);
        lineActive = true;
        UpdateLineToPoint(currentPoint);
        
        agent.isStopped = false;
        if (agent.enabled && agent.isOnNavMesh)
            agent.SetDestination(currentPoint);

        timer = arriveTimeout;
        repathTimer = repathInterval;

        phase = Phase.MoveToPoint;
    }
    private void TickMoveToPoint()
    {
        var agent = stateManager.jesterNav;
        
        repathTimer -= Time.deltaTime;
        if (repathTimer <= 0f)
        {
            repathTimer = repathInterval;
            if (agent.enabled && agent.isOnNavMesh)
                agent.SetDestination(currentPoint);
        }

        timer -= Time.deltaTime;

        if (HasArrived(agent) || timer <= 0f || agent.pathStatus == NavMeshPathStatus.PathInvalid)
        {
            StartSpinNow();
        }
    }

    private bool HasArrived(NavMeshAgent agent)
    {
        float worldDist = Vector3.Distance(stateManager.transform.position, currentPoint);

        bool closeByRemaining = !agent.pathPending && agent.remainingDistance <= arriveDistance;
        bool closeByWorld = worldDist <= directDistanceArrive;

        float v = velocityArriveThreshold;
        bool basicallyStopped = agent.velocity.sqrMagnitude <= (v * v);
        
        bool pathReadyOrGrace = !agent.pathPending || (arriveTimeout - timer) >= repathGraceTime;

        return
            (pathReadyOrGrace && closeByRemaining && basicallyStopped) ||
            (closeByWorld && basicallyStopped) ||
            (closeByWorld && agent.remainingDistance <= arriveDistance + 0.25f);
    }

    private void StartSpinNow()
    {
        var agent = stateManager.jesterNav;
        var anim = stateManager.jesterAnim;

        agent.isStopped = true;
        agent.velocity = Vector3.zero;
        
        CleanupTelegraph();

        anim.ResetTrigger(SpinHash);
        anim.SetTrigger(SpinHash);

        timer = spinDuration;
        phase = Phase.Spin;
    }

    private void TickSpin()
    {
        timer -= Time.deltaTime;
        if (timer > 0f) return;

        pointsDone++;

        if (pointsDone >= 3)
        {
            phase = Phase.Done;
            stateManager.jesterNav.isStopped = false;
            return;
        }

        timer = betweenPointsDelay;
        phase = Phase.Between;

        stateManager.jesterNav.isStopped = false;
    }

    private void TickBetween()
    {
        timer -= Time.deltaTime;
        if (timer > 0f) return;

        phase = Phase.PickPoint;
    }

    private Vector3 PickValidNavMeshPointNearPlayer()
    {
        Vector3 center = stateManager.playerStats.playerLocation.position;

        for (int t = 0; t < maxPickTries; t++)
        {
            Vector2 dir2D = Random.insideUnitCircle.normalized;
            float dist = Random.Range(minRadius, maxRadius);
            Vector3 candidate = center + new Vector3(dir2D.x, 0f, dir2D.y) * dist;

            if (!NavMesh.SamplePosition(candidate, out NavMeshHit hit, navmeshSampleRadius, NavMesh.AllAreas))
                continue;

            if (requireCompletePath && !HasCompletePath(hit.position))
                continue;

            return hit.position;
        }
        
        if (NavMesh.SamplePosition(center, out NavMeshHit fallback, navmeshSampleRadius, NavMesh.AllAreas))
        {
            if (!requireCompletePath || HasCompletePath(fallback.position))
                return fallback.position;
        }

        return stateManager.transform.position;
    }

    private bool HasCompletePath(Vector3 destination)
    {
        NavMeshPath path = new NavMeshPath();
        bool found = NavMesh.CalculatePath(stateManager.transform.position, destination, NavMesh.AllAreas, path);
        return found && path.status == NavMeshPathStatus.PathComplete;
    }
    private void SpawnTelegraph(Vector3 point)
    {
        CleanupTelegraph();

        GameObject prefab = stateManager.spinSlashIndicator;
        if (prefab == null) return;
        telegraphInstance = Object.Instantiate(prefab, point, Quaternion.identity);
        
    }

    private void CleanupTelegraph()
    {
        if (telegraphInstance != null)
        {
            Object.Destroy(telegraphInstance);
            telegraphInstance = null;
        }
        HideLine();
    }
    private void UpdateLineEveryFrame()
    {
        LineRenderer lr = stateManager.lineRenderer;

        if (lr.positionCount < 2)
            lr.positionCount = 2;
        
        if (lr.useWorldSpace)
            lr.SetPosition(0, lr.transform.position);
        
        UpdateLineToPoint(currentPoint);
    }

    private void UpdateLineToPoint(Vector3 targetWorldPoint)
    {
        LineRenderer lr = stateManager.lineRenderer;

        if (lr.positionCount < 2)
            lr.positionCount = 2;

        if (lr.useWorldSpace)
        {
            lr.SetPosition(1, targetWorldPoint);
        }
        else
        {
            lr.SetPosition(1, lr.transform.InverseTransformPoint(targetWorldPoint));
        }
    }

    private void HideLine()
    {
        LineRenderer lr = stateManager.lineRenderer;

        if (lr.positionCount < 2)
            lr.positionCount = 2;
        
        if (lr.useWorldSpace)
        {
            Vector3 p = lr.transform.position;
            lr.SetPosition(0, p);
            lr.SetPosition(1, p);
        }
    }
    
}
