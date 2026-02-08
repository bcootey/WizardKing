using UnityEngine;
using UnityEngine.AI;

public class DragonKnightStateManager : MonoBehaviour
{
    private DragonKnightBaseState currentState;
    public Animator knightAnim;
    public NavMeshAgent knightAgent;
    [Header("NavMeshSettings")] 
    public float defaultSpeed;
    public float defaultAcceleration;
    public float defaultStoppingDistance;
    [Header("References")]
    public PlayerStats playerStats;
    public LookAtPlayer lookAtPlayer;
    [Header("TriggerDetectors")]
    public TriggerDetector playerDistanceDetector;
    public TriggerDetector playerCombatStateDetector;
    public TriggerDetector playerMeleeDetector;

    [Header("Combat")] 
    public float primaryAttackCooldown;
    public float primaryAttackCooldownTimer;
    public bool firstPrimaryAttackDone;
    

    void Awake()
    {
        knightAgent.stoppingDistance = defaultStoppingDistance;
        knightAgent.speed = defaultSpeed;
        knightAgent.acceleration = defaultAcceleration;
    }
    
    void Start()
    {
        playerStats = PlayerStats.instance;
        knightAgent.updateRotation = false;
        currentState = new DragonKnightIdleState(this);
        currentState.EnterState();
    }
    void FixedUpdate()
    {
        currentState.UpdateState();
    }
    public void SetNextState(DragonKnightBaseState nextState)
    {
        currentState.ExitState();
        currentState = nextState;
        currentState.EnterState();
    }

    public void ReturnToDefaultSpeed()
    {
        knightAgent.speed = defaultSpeed;
    }

    public void ChangeSpeed(float speed)
    {
        knightAgent.speed = speed;
    }
    public void ReturnToDefaultAcceleration()
    {
        knightAgent.acceleration =  defaultAcceleration;
    }

    public void ChangeAcceleration(float acceleration)
    {
        knightAgent.acceleration = acceleration;
    }

    public void SetObjectLookTrue()
    {
        lookAtPlayer.isLooking = true;
    }
    public void SetObjectLookFalse()
    {
        lookAtPlayer.isLooking = false;
    }
    public Vector3 GetPositionInFront(float distance)
    {
        return transform.position + transform.forward * distance;
    }

    public void ReturnToIdleState()
    {
        SetNextState(new DragonKnightIdleState(this));
    }

    public void PrimaryAttack1stAttackDone()
    {
        firstPrimaryAttackDone = true;
    }
    

}
