using UnityEngine;
using System.Collections;
using UnityEngine.Serialization;
using UnityEngine.UI;
public class Health : MonoBehaviour
{
    public static Health instance { get; private set; }
    public PlayerDash playerDash;
    public float defaultInvincibilityFrames;
    public bool canBeHit = true;
    [Header("Effects")]
    public ParticleSystem hitEffect;
    public ParticleSystem gainHealthEffect;
    //circles that highlight when health is gained or lost
    public CanvasGroup healthOrbGainCanvasGroup;
    public CanvasGroup healthOrbLoseCanvasGroup;
    public float flashDuration;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void OnPlayerHit(float damage, float invincibilityFrames)
    {
        if (canBeHit && !playerDash.isDashing)
        {
            StartCoroutine(PlayerHit(damage,invincibilityFrames));
        }
    }

    public void IncreaseHealth(int health)
    {
        PlayerStats.instance.currentHealth += health;
        PlayHealthGainEffect();
        if (PlayerStats.instance.currentHealth > PlayerStats.instance.maxHealth)
        {
            PlayerStats.instance.currentHealth = PlayerStats.instance.maxHealth;
        }
        PlayerStats.instance.UpdateHud();
    }

    public void DecreaseHealth(float health)
    {
        PlayerStats.instance.currentHealth -= health;
        CheckIfDead();
        if (PlayerStats.instance.currentHealth < 0)
        {
            PlayerStats.instance.currentHealth = 0;
        }
        PlayerStats.instance.UpdateHud();
    }

    IEnumerator PlayerHit(float damage, float invincibilityFrames)
    {
        canBeHit = false;
        DecreaseHealth(damage);
        PlayHitEffects();
        if (invincibilityFrames < 0)
        {
            yield return new WaitForSeconds(defaultInvincibilityFrames);
        }
        else
        {
            yield return new WaitForSeconds(invincibilityFrames);
        }
        canBeHit = true;
    }

    public void FlashGreen()
    {
        StartCoroutine(FlashSmooth(healthOrbGainCanvasGroup, flashDuration));
    }

    public void FlashRed()
    {
        StartCoroutine(FlashSmooth(healthOrbLoseCanvasGroup, flashDuration)); 
    }

    private void CheckIfDead()
    {
        if (PlayerStats.instance.currentHealth <= 0)
        {
            //death logic
            Destroy(gameObject);
        }
    }
    private void PlayHitEffects()
    {
        hitEffect.Play();
        FlashRed();
    }

    private void PlayHealthGainEffect()
    {
        gainHealthEffect.Play();
        FlashGreen();
    }
    private IEnumerator FlashSmooth(CanvasGroup cg, float duration)
    {

        float half = Mathf.Max(0.0001f, duration * 0.5f);
        
        yield return FadeAlpha(cg, 0f, 1f, half);
        
        yield return FadeAlpha(cg, 1f, 0f, half);
    }

    private IEnumerator FadeAlpha(CanvasGroup cg, float from, float to, float time)
    {
        cg.alpha = from;

        float t = 0f;
        while (t < time)
        {
            t += Time.unscaledDeltaTime;
            float u = Mathf.Clamp01(t / time);
            
            float eased = Mathf.SmoothStep(0f, 1f, u);

            cg.alpha = Mathf.Lerp(from, to, eased);
            yield return null;
        }

        cg.alpha = to;
    }
    
}