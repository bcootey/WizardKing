using UnityEngine;

public class SpectreOrbitState : SpectreBaseState
{
    // 8 directions
    static readonly Vector3[] UnitRing =
    {
        new Vector3( 0f, 0f,  1f), // N
        new Vector3( 1f, 0f,  1f).normalized, // NE
        new Vector3( 1f, 0f,  0f), // E
        new Vector3( 1f, 0f, -1f).normalized, // SE
        new Vector3( 0f, 0f, -1f), // S
        new Vector3(-1f, 0f, -1f).normalized, // SW
        new Vector3(-1f, 0f,  0f), // W
        new Vector3(-1f, 0f,  1f).normalized, // NW
    };

    int index;          // which point we’re going to
    float enterTime;    // small grace time to avoid instant flip back to chase
    public SpectreOrbitState(SpectreStateManager manager) : base(manager) { }
    public override void EnterState()
    {
        enterTime = Time.time;
        
        stateManager.rb.linearVelocity = Vector3.zero;
        
        stateManager.clockwise = Random.value > 0.5f;

        // start at the closest orbit point to our current position
        index = FindClosestIndex();
    }
    public override void UpdateState()
    {
        Transform player = stateManager.playerStats.playerLocation;

        Vector3 playerPos = player.position;
        Vector3 ghostPos  = stateManager.transform.position;
        
        float planarDist = PlanarDistance(ghostPos, playerPos);

        if (Time.time - enterTime > 0.2f && planarDist >= stateManager.disengageDistance)
        {
            stateManager.SetNextState(new SpectreFollowState(stateManager));
            return;
        }

        // Compute the current target point on the ring
        Vector3 ringOffset = UnitRing[index] * stateManager.orbitRadius;
        Vector3 desiredPos = playerPos + ringOffset;
        
        if (stateManager.keepHeightOffset)
            desiredPos.y = playerPos.y + stateManager.heightOffset;
        else
            desiredPos.y = ghostPos.y;

        //move toward that orbit point
        Vector3 toDesired = desiredPos - ghostPos;

        //if we reached the point, advance to the next one
        if (toDesired.magnitude <= 0.35f)
        {
            index = NextIndex(index, stateManager.clockwise);
            ringOffset = UnitRing[index] * stateManager.orbitRadius;
            desiredPos = playerPos + ringOffset;
            if (stateManager.keepHeightOffset)
                desiredPos.y = playerPos.y + stateManager.heightOffset;

            toDesired = desiredPos - ghostPos;
        }

        // velocity toward the point
        Vector3 desiredVel = toDesired.normalized * stateManager.orbitSpeed;

        // smooth acceleration
        stateManager.rb.linearVelocity = Vector3.MoveTowards(
            stateManager.rb.linearVelocity,
            desiredVel,
            stateManager.accel * Time.fixedDeltaTime
        );
        
    }
    public override void ExitState()
    {
        
    }
    int FindClosestIndex()
    {
        Transform player = stateManager.playerStats.playerLocation;

        Vector3 fromPlayer = stateManager.transform.position - player.position;
        fromPlayer.y = 0f;

        if (fromPlayer.sqrMagnitude < 0.0001f)
            return 0;

        fromPlayer.Normalize();

        // pick ring direction with max dot product (most aligned)
        int best = 0;
        float bestDot = -999f;

        for (int i = 0; i < UnitRing.Length; i++)
        {
            float d = Vector3.Dot(fromPlayer, UnitRing[i]);
            if (d > bestDot)
            {
                bestDot = d;
                best = i;
            }
        }

        return best;
    }
    int NextIndex(int current, bool clockwise)
    {
        // clockwise means go forward through the array (wrap around)
        return clockwise
            ? (current + 1) % 8
            : (current + 7) % 8; // -1 mod 8
    }

    float PlanarDistance(Vector3 a, Vector3 b)
    {
        a.y = 0f;
        b.y = 0f;
        return Vector3.Distance(a, b);
    }
}
