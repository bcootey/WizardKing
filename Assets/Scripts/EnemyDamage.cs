using System;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public EnemyDamageSources damageSource;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Health.instance.OnPlayerHit(damageSource.damage,damageSource.invincibilityFrames);
        }
    }
}
