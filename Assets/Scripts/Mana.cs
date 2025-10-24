using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mana : MonoBehaviour
{
    public float manaRegenSpeed;
    
    public bool CanUseSpell(Spell spell)
    {
        if (spell.mana > PlayerStats.instance.currentMana)
        {
            return false;
        }
        return true;
    }
    public void IncreaseMana(int mana)
    {
        PlayerStats.instance.currentMana += mana;
        if (PlayerStats.instance.currentMana > PlayerStats.instance.maxMana)
        {
            PlayerStats.instance.currentMana = PlayerStats.instance.maxMana;
        }
        PlayerStats.instance.UpdateHud();
    }
    public void DecreaseMana(int mana)
    {
        PlayerStats.instance.currentMana -= mana;
        PlayerStats.instance.UpdateHud();
    }
    void Update()
    {
        if (PlayerStats.instance.currentMana < PlayerStats.instance.maxMana)
        {
            PlayerStats.instance.currentMana += Time.deltaTime * manaRegenSpeed;
            if (PlayerStats.instance.currentMana > PlayerStats.instance.maxMana)
            {
                PlayerStats.instance.currentMana = PlayerStats.instance.maxMana;
            }
        }
    }
}
