using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightHandStateManager : MonoBehaviour
{
    private RightHandBaseState currentState;
    public Animator rightHandAnim;
    public PlayerStats playerStats;
    //damage of the next attack to happen. the number is a modification of the weapon base damage from playerstats
    public float currentAttackDamage;
    public int currentAttackDamageNormalized;
    public AttackDamage attackDamage;
    public bool swinging;
    void Start()
    {
        currentState = new RightHandIdleState(this);
        currentState.EnterState();
    }
    void Update()
    {
        currentState.UpdateState();
    }
    public void SetNextState(RightHandBaseState nextState)
    {
        currentState.ExitState();
        currentState = nextState;
        currentState.EnterState();
    }
    public void SetSwingingTrue()
    {
        swinging = true;
    }
    public void SetSwingingFalse()
    {
        swinging = false;
    }
    public void ReturnToIdle()
    {
        SetNextState(new RightHandIdleState(this));
    }
    public void UpdateAttackDamage(int damage)
    {
        attackDamage.damage = damage;
    }
}
