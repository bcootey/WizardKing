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
    public GameObject shieldObject;
    public int maxShieldHealth;
    public int shieldHealth;
    public Renderer shieldRenderer;
    public GameObject shieldBreakParticles;
    private MaterialPropertyBlock materialPropertyBlock;
    
    // private backing fields
    [Header("health")]
    [SerializeField] private int _maxHealth = 100;
    [SerializeField] private int _currentHealth;

    public int currentHealth => _currentHealth;
    public int maxHealth => _maxHealth;
    
    [Header("Gore Effects")]
    public GameObject goreEffect;
    public Transform goreSpawnPoint;
    [Header("Hit Materials")]
    public RendererMaterialSet[] hitMaterialSets;

    [SerializeField] private float hitFlashDuration = 0.05f;
    
    [Header("Pickups")]
    public GameObject[] pickup;
    public Transform pickupSpawnPoint;
    [Header("Experience")]
    public int experienceOnKill;

    [Header("HealthBar")] 
    public EnemyHealthBar enemyHealthBar;
    [Header("Coins")] 
    public GameObject coinPursePrefab;
    public Transform coinPurseSpawnPoint;
    [SerializeField] private int coinsMin;
    [SerializeField] private int coinsMax;
    public int CoinsMin { get => coinsMin; set => coinsMin = value; }
    public int CoinsMax { get => coinsMax; set => coinsMax = value; }
    
    [Header("DamageNumbers")]
    public GameObject damageNumberPrefab;
    [Header("SoundEffects")]
    public GameObject hitAudioSource;
    public AudioClip[] hitEffects;
    
    private bool isDead = false;
    private Collider[] colliders;
    
    //sets up the shaders for the shield
    void Awake()
    {
        materialPropertyBlock = new MaterialPropertyBlock();
        colliders = GetComponentsInChildren<Collider>();
    }
    void Start()
    {
        _currentHealth = _maxHealth;
        shieldHealth = maxShieldHealth;
        //cache original materials for every renderer
        foreach (var set in hitMaterialSets)
        {
            if (set.renderer == null) continue;
            set.originalMaterials = set.renderer.materials;
        }
        //sets the current state of enemy based on whats in the inspector
        SetHealthState();
        currentState.EnterState();
    }
    void Update()
    {
        currentState.UpdateState();
        CheckIfDead();
    }
    public void SetNextState(EnemyHealthBaseState nextState)
    {
        currentState.ExitState();
        currentState = nextState;
        currentState.EnterState();
    }
    public void TakeDamage(int amount)
    {
        if (isDead) return;
        
        _currentHealth -= amount;
        enemyHealthBar.TakeDamage(amount);
        SpawnDamageNumbers(amount);
        ChangeToHitMaterial();
        PlayHitSoundEffect();
        HitStop.instance.Stop(.1f);
        CheckIfDead();
    }
    public void CheckIfDead()
    {
        
        if (_currentHealth <= 0)
        {
            isDead = true;
            
            // Prevent any more triggers / hits this frame
            if (colliders != null)
            {
                foreach (var c in colliders)
                    if (c) c.enabled = false;
            }
            
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
        if (isDead) return;
        
        IDamageDealer dealer = other.GetComponentInParent<IDamageDealer>(); //tries to find dealer component in children or parent
        if (dealer == null) dealer = other.GetComponentInChildren<IDamageDealer>();
        if (dealer == null) return;
        
        GameObject enemyRoot = gameObject;
        
        if (dealer is AttackDamage ad && !ad.CanHit(enemyRoot)) return;
        if (dealer is SpellDamage sd) //also checks if its enabled because of reflecting projectiles
        {
            if (!sd.enabled) return;
            if (!sd.CanHit(enemyRoot)) return;
        }
        
        TryToTakeDamage(dealer.Damage, dealer);
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
                //if enemy has a shield it takes damage
                if (shieldState == EnemyShieldState.Shield)
                {
                    shieldHealth -= damage;
                    UpdateShieldMaterial();
                }
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

    private void ChangeToHitMaterial()
    {
        StopCoroutine(nameof(HitFlashRoutine));
        StartCoroutine(HitFlashRoutine());
    }

    private IEnumerator HitFlashRoutine()
    {
        foreach (var set in hitMaterialSets)
        {
            if (set.renderer == null || set.originalMaterials == null) continue;

            Material[] mats = set.renderer.materials;
            
            int count = Mathf.Min(mats.Length, set.originalMaterials.Length);
            for (int i = 0; i < count; i++)
            {
                if (set.hitMaterials != null &&
                    i < set.hitMaterials.Length &&
                    set.hitMaterials[i] != null)
                {
                    mats[i] = set.hitMaterials[i];
                }
                else
                {
                    mats[i] = set.originalMaterials[i];
                }
            }

            set.renderer.materials = mats;
        }

        yield return new WaitForSeconds(hitFlashDuration);
        
        foreach (var set in hitMaterialSets)
        {
            if (set.renderer == null || set.originalMaterials == null) continue;

            set.renderer.materials = set.originalMaterials;
        }
    }
    private void PlayHitSoundEffect()
    {
        int rand  = Random.Range(0, hitEffects.Length);
        GameObject soundEffect = Instantiate(hitAudioSource,goreEffect.transform.position,goreEffect.transform.rotation);
        soundEffect.GetComponent<AudioSource>().clip = hitEffects[rand];
        soundEffect.GetComponent<AudioSource>().Play();
    }

    private void SetHealthState()
    {
        if (shieldState == EnemyShieldState.Neutral)
        {
            currentState = new EnemyHealthNeutralState(this);
        }
        else if (shieldState == EnemyShieldState.Shield)
        {
            currentState = new EnemyHealthShieldState(this);
        }
    }

    private void UpdateShieldMaterial()
    {
        float value = (float)shieldHealth / (float)maxShieldHealth;
        shieldRenderer.GetPropertyBlock(materialPropertyBlock);
        materialPropertyBlock.SetFloat("_CrackAmount", 1f - value);
        shieldRenderer.SetPropertyBlock(materialPropertyBlock);
    }
    
}
[System.Serializable]
public class RendererMaterialSet
{
    public Renderer renderer;
    
    public Material[] hitMaterials;
    
    [HideInInspector] public Material[] originalMaterials;
}

