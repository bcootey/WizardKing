using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellDamage : MonoBehaviour, IDamageDealer
{
    public Spell spell;
    [HideInInspector]
    public int damage;

    public int Damage => damage;

    void Awake()
    {
        damage = Mathf.RoundToInt(PlayerStats.instance.baseMagicDamage * (float)spell.damageMod);
    }
}

