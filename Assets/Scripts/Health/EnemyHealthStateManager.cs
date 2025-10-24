using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthStateManager : MonoBehaviour, IHealth
{
    private EnemyHealthBaseState currentState;
    public enum EnemyShieldState
    {
        Neutral,
        Shield,
        RuneShield
    }
    
    [Header("Enemy Shield State")]
    public EnemyShieldState shieldState = EnemyShieldState.Neutral;
    
    // Private backing fields
    [SerializeField] private int _maxHealth = 100;
    [SerializeField] private int _currentHealth;

    public int currentHealth => _currentHealth;
    public int maxHealth => _maxHealth;
    
    [Header("Gore Effects")]
    public GameObject goreEffect;
    public Transform goreSpawnPoint;
    
    [Header("Pickups")]
    public GameObject[] pickup;
    public Transform pickupSpawnPoint;
    [Header("Experience")]
    public int experienceOnKill;
    
    void Start()
    {
        _currentHealth = _maxHealth;
        currentState = new EnemyHealthNeutralState(this);
        currentState.EnterState();
    }
    void Update()
    {
        currentState.UpdateState();
    }
    public void SetNextState(EnemyHealthBaseState nextState)
    {
        currentState.ExitState();
        currentState = nextState;
        currentState.EnterState();
    }
    public void TakeDamage(int amount)
    {
        _currentHealth -= amount;
        HitStop.instance.Stop(.1f);
        CheckIfDead();
    }
    public void CheckIfDead()
    {
        if (_currentHealth <= 0)
        {
            //make death logic
            Instantiate(goreEffect,goreSpawnPoint.position, goreSpawnPoint.rotation);
            Debug.Log("Im dead");
            SpawnPickups();
            GiveExperience();
            //stupid death logic
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        IDamageDealer dealer = other.GetComponent<IDamageDealer>();
        if (dealer != null)
        {
            int damage = dealer.Damage;
            TryToTakeDamage(damage,dealer);
        }
    }

    private void SpawnPickups()
    {
        int range = PlayerStats.instance.luck / 10;
        int rand = Random.Range(0, 11);
        if (rand <= range)
        {
            int rand2 = Random.Range(0, pickup.Length);
            if (rand2 == 0)
            {
                SpawnPickupAndAddForce(rand2);
            }
            else
            {
                SpawnPickupAndAddForce(rand2);
            }
        }
    }

    private Vector3 GetRandomSpawnDirection()
    {
        Vector3 forceDirection = new Vector3(Random.Range(0, 2), Random.Range(0, 2), Random.Range(0, 2));
        return forceDirection;
    }

    private void SpawnPickupAndAddForce(int random)
    {
        GameObject spawnedPickup = Instantiate(pickup[random], pickupSpawnPoint.position, Quaternion.identity);
        Rigidbody rb = spawnedPickup.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(GetRandomSpawnDirection().normalized * 6, ForceMode.Impulse);
        }
    }

    private void TryToTakeDamage(int damage, IDamageDealer dealer)
    {
        switch (dealer)
        {
            case AttackDamage attackDamage: //damage is always applied if its a melee attack as shields dont block it
                TakeDamage(damage);
                break;
            case SpellDamage spellDamage: //damage is only applied if the enemy is in the neutral state
                if (shieldState == EnemyShieldState.Neutral)
                {
                    TakeDamage(damage);
                }
                else if (shieldState == EnemyShieldState.Shield)
                {
                    return;
                }
                break;
        }
    }

    private void GiveExperience()
    {
        Experience experience = GameObject.Find("Pyromancer").GetComponent<Experience>();
        experience.GainExperience(experienceOnKill);
    }
}

