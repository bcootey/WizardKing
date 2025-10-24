using UnityEngine;

[CreateAssetMenu(fileName = "EnemyDamageSources", menuName = "ScriptableObjects/EnemyDamageSources")]
public class EnemyDamageSources : ScriptableObject
{
    public float damage;
    [Tooltip("Keep at -1 for defualt invincibility frames")]
    public float invincibilityFrames = -1; // how long the player remains untouchable after they've been hit
    //can potentially add more things here like knockback, d
}
