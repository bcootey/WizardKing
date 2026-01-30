using UnityEngine;

public class SpectreStateManager : MonoBehaviour
{
    private SpectreBaseState currentState;
    [HideInInspector]
    public PlayerStats playerStats;
    public Animator spectreAnimator;
    [Header("Hover")]
    public bool keepHeightOffset = true;
    public float heightOffset = 1.2f;
    public float verticalStrength = 6f;
    [Header("Movement")]
    public float chaseSpeed = 6f;
    public float orbitSpeed = 3.5f;
    public float accel = 15f;

    [Header("Orbit Distances")]
    public float orbitRadius = 3f;
    public float engageDistance = 4f;      // switch to orbit when <= this
    public float disengageDistance = 6f;   // switch to follow when >= this
    [Header("Orbit Direction")]
    public bool clockwise = true;
    [Header("RigidBody")]
    public Rigidbody rb;
    [Header("Aggro Range")]
    public float aggroRange = 10f;
    [Header("MeleeRange")]
    public TriggerDetector meleeDetector;
    [Header("Melee Settings")]
    public float meleeCooldown = 2f;
    public float meleeCooldownTimer;
    [Header("Effects")]
    public ParticleSystem slashEffect;
    public ParticleSystem slashEffect2;

    private float defaultFloatRadius;
    private float defaultSpeed;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        
        //random value assignments
        orbitRadius = Random.Range(2.6f, 3.1f);
        defaultFloatRadius = orbitRadius;
        orbitSpeed = Random.Range(2.5f, 5f);
        defaultSpeed = orbitSpeed;
        heightOffset = Random.Range(.37f, 1f);
        chaseSpeed = Random.Range(5f, 7f);
        
        //start melee being ready
        meleeCooldownTimer = meleeCooldown;
    }
    void Start()
    {
        playerStats = PlayerStats.instance;
        currentState = new SpectreIdleState(this);
        currentState.EnterState();
    }
    void FixedUpdate()
    {
        currentState.UpdateState();
        meleeCooldownTimer += Time.fixedDeltaTime;
        if (meleeDetector.WasEntered())
        {
            if (meleeCooldownTimer >= meleeCooldown)
            {
                spectreAnimator.SetTrigger("Melee");
                meleeCooldownTimer = 0;
            }
        }
    }
    public void SetNextState(SpectreBaseState nextState)
    {
        currentState.ExitState();
        currentState = nextState;
        currentState.EnterState();
    }

    public void ResetAnimationTriggers()
    {
        spectreAnimator.ResetTrigger("Melee");
    }

    public void AttackPlayer()
    {
        orbitRadius = 1.9f;
        orbitSpeed = 2f;
    }
    public void ReturnToDefaultState()
    {
        orbitRadius = defaultFloatRadius;
        orbitSpeed = defaultSpeed;
    }

    public void PlaySlashEffect()
    {
        slashEffect.Play();
        slashEffect2.Play();
    }
}
