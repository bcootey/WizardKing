using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Spell", menuName = "ScriptableObjects/Spell")]
public class Spell : ScriptableObject
{
    public string spellName;
    public int mana;
    [Header("How much base damage is amplified. 1 for no modification")]
    public double damageMod;
    [Space]
    [Tooltip("Only Applicable if SpawnPoint == Player")]
    public int speed;
    [Space]
    public SpellType spellType;
    public SpawnPoint spawnPoint;
    public GameObject spellReticle;
    public GameObject spellInstance;

    [Header("Doesnt change anything unless its an Ultimate")]
    [Tooltip("in seconds")]
    public int chargingTime;
}
public enum SpellType
{
    Thrown,
    Ultimate
}
public enum SpawnPoint
{
    Player,
    Reticle
}
