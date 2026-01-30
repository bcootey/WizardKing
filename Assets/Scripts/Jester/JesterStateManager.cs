using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class JesterStateManager : MonoBehaviour, IParryable
{
    private JesterBaseState currentState;
    public PlayerStats playerStats;
    public TriggerDetector meleeRange;
    [Header("Animations")]
    public Animator jesterAnim;
    [FormerlySerializedAs("jesterAgent")] [Header("Navigation")]
    public NavMeshAgent jesterNav;
    public TriggerDetector followRange;
    
    [Header("Default settings")]
    public float defaultSpeed;
    public float defaultAcceleration;
    public float defaultStoppingDistance;
    [Header("Effects")] 
    public ParticleSystem slashEffect1;
    public ParticleSystem slashEffect2;
    public ParticleSystem slashEffect3;
    public ParticleSystem slashEffect4;
    public ParticleSystem slashEffect5;
    public ParticleSystem slashEffect6;
    public ParticleSystem dashEffect1;
    public ParticleSystem spinSlashEffect;
    public ParticleSystem spinSlashEffect2;
    [Header("DaggerThrow")]
    public Transform daggerThrowLocation;
    public GameObject daggerThrowPrefab;
    public float daggerThrowSpeed;
    [Header("SpinSlash")]
    public GameObject spinSlashPrefab;
    public GameObject spinSlashIndicator;
    public LineRenderer lineRenderer;
    [Header("gameplay")]
    public float meleeCooldown = 4f;
    public float backflipCooldown = 6f;
    public float spinSlashCooldown = 7f;
    [HideInInspector]
    public float meleeCooldownTimer;
    [HideInInspector]
    public float backflipCooldownTimer;
    [HideInInspector]
    public float spinSlashCooldownTimer;
    [Header("LookAtPlayer")]
    public LookAtPlayer lookAtPlayer;
    [Header("Parry")] 
    public bool isParryable;
    public bool isParried;
    public bool IsParried
    {
        get => isParried;
        set => isParried = value;
    }
    public bool IsParryable
    {
        get => isParryable;
        set => isParryable = value;
    }
    void Start()
    {
        jesterNav.speed = defaultSpeed;
        jesterNav.acceleration = defaultAcceleration;
        jesterNav.updateRotation = false;
        playerStats = PlayerStats.instance;
        currentState = new JesterIdleState(this);
        currentState.EnterState();
    }
    void Update()
    {
        currentState.UpdateState();
    }
    public void SetNextState(JesterBaseState nextState)
    {
        currentState.ExitState();
        currentState = nextState;
        currentState.EnterState();
    }

    public void PlaySlashEffect1()
    {
        slashEffect1.Play();
        slashEffect2.Play();
    }

    public void PlaySlashEffect2()
    {
        slashEffect3.Play();
        slashEffect4.Play();
    }

    public void PlaySlashEffect3()
    {
        slashEffect5.Play();
        slashEffect6.Play();
    }

    public void PlayDashEffect1()
    {
        dashEffect1.Play();
    }

    public void PlaySpinSlashEffect()
    {
        spinSlashEffect.Play();
        spinSlashEffect2.Play();
    }
    public void ChangeSpeed(float speed)
    {
        jesterNav.speed = speed;
        //wizardNav.acceleration = speed/2;
    }
    public void ResetSpeed()
    {
        jesterNav.speed = defaultSpeed;
        jesterNav.acceleration = defaultAcceleration;
    }
    public void ResetAnimations()
    {
        jesterAnim.ResetTrigger("Walk");
        jesterAnim.ResetTrigger("Idle");
        jesterAnim.ResetTrigger("Backflip");
        jesterAnim.ResetTrigger("Melee");
        jesterAnim.ResetTrigger("Spin");
        
    }

    public void ReturnToFollow()
    {
        SetNextState(new JesterFollowState(this));
    }

    public void ThrowDaggerProjectile()
    {
        GameObject thrownDagger = Instantiate(daggerThrowPrefab, daggerThrowLocation.position, daggerThrowLocation.rotation);
        Rigidbody rb = thrownDagger.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(daggerThrowLocation.forward * daggerThrowSpeed, ForceMode.Impulse);
        }
    }
    public void SpawnSpinDamagePrefab()
    {
        Instantiate(spinSlashPrefab, spinSlashEffect2.transform.position, Quaternion.identity);
    }
    public void SetIsParryable()
    {
        IsParryable = true;
    }

    public void SetIsNotParryable()
    {
        IsParryable = false;
    }
}
