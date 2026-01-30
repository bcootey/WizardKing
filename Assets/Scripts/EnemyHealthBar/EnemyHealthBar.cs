using UnityEngine;

public class EnemyHealthBar : MonoBehaviour
{
    [Header("References")]
    public Transform healthBarRed;
    public Transform healthBarYellow;
    public EnemyHealthStateManager enemyHealthStateManager;

    [Header("Health")]
    public float maxHealth;
    public float currentHealth;

    [Header("Bar Scale")]
    public float maxBarWidth = 0.9648001f;
    public float barYScale = 1f;
    public float barZScale = 0.623f;

    [Header("Chip / Delayed Settings")]
    public float chipDelay = 0.35f;     // how long after last hit before yellow starts moving
    public float chipSpeed = 10f;       // how fast yellow catches up once it starts

    private float lastHitTime = -999f;
    
    void Start()
    {
        maxHealth = enemyHealthStateManager.maxHealth;
        currentHealth = maxHealth;
        SetRedImmediate();
        SetYellowImmediate();
    }
    void Update()
    {
        // If we haven't been hit recently, let yellow catch up to red
        if (Time.time >= lastHitTime + chipDelay)
        {
            float targetRatio = GetHealthRatio();
            float targetX = RatioToX(targetRatio);

            Vector3 yScale = healthBarYellow.localScale;
            yScale.x = Mathf.Lerp(yScale.x, targetX, Time.deltaTime * chipSpeed);
            yScale.y = barYScale;
            yScale.z = barZScale;
            healthBarYellow.localScale = yScale;
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0f, maxHealth);
        lastHitTime = Time.time;

        // Red updates immediately on hit
        SetRedImmediate();

        // Yellow does NOT update here (it "freezes" while hits are happening)
    }

    float GetHealthRatio()
    {
        return (maxHealth <= 0f) ? 0f : (currentHealth / maxHealth);
    }

    float RatioToX(float ratio)
    {
        return Mathf.Clamp01(ratio) * maxBarWidth;
    }

    void SetRedImmediate()
    {
        float x = RatioToX(GetHealthRatio());
        healthBarRed.localScale = new Vector3(x, barYScale, barZScale);
    }

    void SetYellowImmediate()
    {
        float x = RatioToX(GetHealthRatio());
        healthBarYellow.localScale = new Vector3(x, barYScale, barZScale);
    }
}
