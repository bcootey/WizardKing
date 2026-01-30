using UnityEngine;

public class HitBoxEnable : MonoBehaviour //placed on player sword hit box to activate animation events
{
    private AttackDamage attackDamage;

    void Awake()
    {
        attackDamage = GetComponent<AttackDamage>();
    }

    void OnEnable()
    {
        if (attackDamage != null)
        {
            attackDamage.BeginAttack();
        }
    }
}
