using UnityEngine;

public class ParryableProjectile : MonoBehaviour, IParryable
{
    public Rigidbody rb;
    public float speed;
    private Vector3 playerLookingDirection;
    [Header("Parry")] 
    public bool isParryable;
    public bool isParried;
    private Camera cam;
    [Header("Projectile Damage")] 
    public SpellDamage spellDamage;
    public bool IsParried
    {
        get => isParried;
        set => isParried = value;
    }
    public bool IsParryable
    {
        get => isParryable;
        set => isParryable = value;
    }
    public void SetIsParryable()
    {
        IsParryable = true;
    }

    public void SetIsNotParryable()
    {
        IsParryable = false;
    }

    void Awake()
    {
        SetIsParryable();
        cam = Camera.main;
        spellDamage.enabled = false;
    }
    
    void Update()
    {
        if (IsParried) //sends backwards if parried
        {
            rb.linearVelocity = Vector3.zero;
            rb.AddForce(cam.transform.forward * speed, ForceMode.Impulse);
            spellDamage.enabled = true;
            IsParried = false;
        }
    }
}
