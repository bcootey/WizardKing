using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EvilWizardStateManager : MonoBehaviour
{
    private EvilWizardBaseState currentState;
    public Animator wizardAnim;
    public PlayerStats playerStats;
    public NavMeshAgent wizardNav;
    public InRange spellInRange;
    public TriggerDetector meleeInRange;
    public TriggerDetector swingRange;
    [Header("spells")]
    public EnemySpells[] spells;
    public EnemySpells currentSpell;
    public Transform castingLocation;
    public Transform sigilSpawnPoint;
    [Header("navigation")]
    public float defaultSpeed;
    public float defaultAcceleration;
    [Header("Effects")]
    public ParticleSystem dashEffect;
    public ParticleSystem slashEffect;
    public ParticleSystem slashEffect2;
    void Start()
    {
        wizardNav.speed = defaultSpeed;
        wizardNav.acceleration = defaultAcceleration;
        wizardNav.updateRotation = false;
        playerStats = PlayerStats.instance;
        currentState = new EvilWizardIdleState(this);
        currentState.EnterState();
    }
    void Update()
    {
        currentState.UpdateState();
    }
    public void SetNextState(EvilWizardBaseState nextState)
    {
        currentState.ExitState();
        currentState = nextState;
        currentState.EnterState();
    }
    
    public void ReturnToIdle()
    {
        SetNextState(new EvilWizardIdleState(this));
    }
    public void ChooseSpell()
    {
        int rand = Random.Range(0, spells.Length);
        currentSpell = spells[rand];
    }
    public void CastSpell()
    {
        if(currentSpell.spellType == SpellSpawnType.Instant)
        {
            Instantiate(currentSpell.spellInstance, castingLocation.position, castingLocation.rotation); 
        }
        else
        {
            GameObject castedSpell = Instantiate(currentSpell.spellInstance, castingLocation.position, castingLocation.rotation);
            Rigidbody rb = castedSpell.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(castingLocation.forward * currentSpell.speed, ForceMode.Impulse);
            }
        }
    }
    public void SpawnSigil()
    {
        Instantiate(currentSpell.spellSigil, sigilSpawnPoint.position, sigilSpawnPoint.rotation);
    }
    public void ChangeSpeed(float speed)
    {
        wizardNav.speed = speed;
        //wizardNav.acceleration = speed/2;
    }
    public void ResetSpeed()
    {
        wizardNav.speed = defaultSpeed;
        wizardNav.acceleration = defaultAcceleration;
    }
    public void PlayDashEffect()
    {
        dashEffect.Play();
    }

    public void PlaySlashEffect()
    {
        slashEffect.Play();
    }

    public void PlaySlashEffect2()
    {
        slashEffect2.Play();
    }

}
