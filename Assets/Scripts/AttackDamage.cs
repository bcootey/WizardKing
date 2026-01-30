using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDamage : MonoBehaviour, IDamageDealer
{
    public int damage;
    public int Damage => damage;
    
    private readonly HashSet<int> hitTargets = new HashSet<int>();
    public void BeginAttack() //calls in the swing animation when damage can be dealt
    {
        hitTargets.Clear();
    }
    public bool CanHit(GameObject enemyRoot) //checks to see if the enemy is already added to the hit list and if not then adds it
    {
        int id = enemyRoot.GetInstanceID();
        if (hitTargets.Contains(id)) return false;

        hitTargets.Add(id);
        return true;
    }
    
}
