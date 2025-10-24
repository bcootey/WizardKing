using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "EnemySpells", menuName = "ScriptableObjects/EnemySpells")]
public class EnemySpells : ScriptableObject
{
    public string spellName;
    [Tooltip("Only Applicable if SpawnType == Projectile")]
    public int speed;
    [Space]
    public SpellSpawnType spellType;
    public GameObject spellInstance;
    public GameObject spellSigil;
}
public enum SpellSpawnType
{
    Instant,
    Projectile
}
