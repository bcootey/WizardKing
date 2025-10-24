using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCasting : MonoBehaviour
{
    public Transform playerSpellSpawn;
    public void CastSpellReticle(Spell spell,Vector3 spawnPoint)
    {
        Instantiate(spell.spellInstance, spawnPoint, Quaternion.identity);
    }
    public void CastSpellPlayer(Spell spell)
    {
        GameObject castedSpell = Instantiate(spell.spellInstance, playerSpellSpawn.position, playerSpellSpawn.rotation);
        Rigidbody rb = castedSpell.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(playerSpellSpawn.forward * spell.speed, ForceMode.Impulse);
        }
    }
}
