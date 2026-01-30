using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellDamage : MonoBehaviour, IDamageDealer
{
    public Spell spell;
    [HideInInspector]
    public int damage;
    public int Damage => damage;

    private readonly HashSet<int> hitTargets = new HashSet<int>(); // a single projectile can only hit once
    
    void Awake()
    {
        damage = Mathf.RoundToInt(PlayerStats.instance.baseMagicDamage * (float)spell.damageMod);
    }
    
    public bool CanHit(GameObject enemyRoot)
    {
        int id = enemyRoot.GetInstanceID();
        if (hitTargets.Contains(id)) return false;

        hitTargets.Add(id);
        return true;
    }
    public void ResetHitTargets() //called on certain spells to allow them to hit multiple times
    {
        hitTargets.Clear();
    }
}

