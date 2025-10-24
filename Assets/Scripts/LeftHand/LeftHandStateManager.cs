using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LeftHandStateManager : MonoBehaviour
{
    private LeftHandBaseState currentState;
    public Animator leftHandAnim;
    public Spell currentSpell;
    public Spell currentUltimateSpell;
    public bool casting;
    public SpellCasting spellCasting;
    [Header("ReticleRay")]
    public Camera mainCamera;
    public LayerMask wallLayer;
    public GameObject reticleInstance;
    public RaycastHit hit;
    public Vector3 spellSpawnPoint;
    [Header("Ultimate Bar")]
    public Slider ultimateChargeBar;
    //public FPScontroller FPScontroller;
    [Header("mana")]
    public Mana mana;

    void Start()
    {
        mainCamera = Camera.main;
        //
        currentState = new LeftHandIdleState(this);
        currentState.EnterState();
    }
    void Update()
    {
        currentState.UpdateState();
    }
    public void SetNextState(LeftHandBaseState nextState)
    {
        currentState.ExitState();
        currentState = nextState;
        currentState.EnterState();
    }
    public void OnAnimationFinish()
    {
        SetNextState(new LeftHandIdleState(this));
    }
    public void CastSpell(Spell spell,Vector3 spellSpawnPoint)
    {
        
        //decides if the spell is spawned at the player or at the aiming reticle
        Debug.Log("" +  spell.name);
        //is casted at the reticle
        if (spell.spawnPoint == SpawnPoint.Reticle)
        {
            spellCasting.CastSpellReticle(spell, spellSpawnPoint);
        }
        //is casted at the players camera position
        else
        {
            spellCasting.CastSpellPlayer(spell);
        }
    }
    public void CastSpellAnimationEvent()
    {
        CastSpell(currentSpell,spellSpawnPoint);
    }
    public void CastUltimateSpellAnimationEvent()
    {
        CastSpell(currentUltimateSpell, spellSpawnPoint);
    }

}
