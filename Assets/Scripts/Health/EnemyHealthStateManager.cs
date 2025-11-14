using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class EnemyHealthStateManager : MonoBehaviour, IHealth, IDropsCoins
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

    [Header("Coins")] 
    public GameObject coinPursePrefab;
    public Transform coinPurseSpawnPoint;
    [SerializeField] private int coinsMin;
    [SerializeField] private int coinsMax;
    public int CoinsMin { get => coinsMin; set => coinsMin = value; }
    public int CoinsMax { get => coinsMax; set => coinsMax = value; }
    
    [Header("DamageNumbers")]
    public GameObject damageNumberPrefab;

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
        SpawnDamageNumbers(amount);
        HitStop.instance.Stop(.1f);
        CheckIfDead();
    }
    public void CheckIfDead()
    {
        if (_currentHealth <= 0)
        {
            EnemyDeath();
            Destroy(this.gameObject);
        }
    }
    public void DropCoins()
    {
        int coins = Random.Range(CoinsMin, CoinsMax + 1);
        SpawnCoinPurseAndAddForce(coins);
    }

    private void EnemyDeath() //what happens when the enemy dies
    {
        Instantiate(goreEffect,goreSpawnPoint.position, goreSpawnPoint.rotation);
        SpawnPickups();
        DropCoins();
        GiveExperience();
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
    private void SpawnCoinPurseAndAddForce(int coin)
    {
        GameObject spawnedPickup = Instantiate(coinPursePrefab, coinPurseSpawnPoint.position, Quaternion.identity);
        Rigidbody rb = spawnedPickup.GetComponent<Rigidbody>();
        spawnedPickup.GetComponentInChildren<CoinPurse>().coinAmount = coin;
        if (rb != null)
        {
            rb.AddForce(GetRandomSpawnDirection().normalized * 3, ForceMode.Impulse);
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

    private void SpawnDamageNumbers(int damage)
    {
        float radius = 0.5f;
        
        Vector3 randomDir = Random.onUnitSphere;
        
        randomDir.y = Mathf.Abs(randomDir.y) * 0.4f;
        Vector3 spawnPos = goreSpawnPoint.transform.position + randomDir.normalized * radius;
        GameObject damageNumbers = Instantiate(damageNumberPrefab, spawnPos, goreSpawnPoint.transform.rotation);

        TextMeshPro damageText = damageNumbers.GetComponentInChildren<TextMeshPro>();
        damageText.text = damage.ToString();
    }
    
}

