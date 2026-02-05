using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mana : MonoBehaviour
{
    public float manaRegenSpeed;
    public static Mana instance { get; private set; }
    [Header("Effects")]
    public ParticleSystem manaGainEffect;
    public CanvasGroup manaOrbGainEffect;
    public CanvasGroup manaOrbLoseEffect;
    public float flashDuration;
    private void Awake()
    {
        // Enforce singleton pattern
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }
    public bool CanUseSpell(Spell spell)
    {
        if (spell.mana > PlayerStats.instance.currentMana)
        {
            return false;
        }
        return true;
    }
    public void IncreaseMana(int mana)
    {
        PlayerStats.instance.currentMana += mana;
        PlayManaGainEffect();
        if (PlayerStats.instance.currentMana > PlayerStats.instance.maxMana)
        {
            PlayerStats.instance.currentMana = PlayerStats.instance.maxMana;
        }
        PlayerStats.instance.UpdateHud();
    }
    public void DecreaseMana(int mana)
    {
        PlayerStats.instance.currentMana -= mana;
        PlayerStats.instance.UpdateHud();
        PlayManaLoseEffect();
    }
    void Update()
    {
        if (PlayerStats.instance.currentMana < PlayerStats.instance.maxMana)
        {
            PlayerStats.instance.currentMana += Time.deltaTime * manaRegenSpeed;
            if (PlayerStats.instance.currentMana > PlayerStats.instance.maxMana)
            {
                PlayerStats.instance.currentMana = PlayerStats.instance.maxMana;
            }
        }
    }
    public void FlashGreen()
    {
        StartCoroutine(FlashSmooth(manaOrbGainEffect, flashDuration));
    }

    public void FlashRed()
    {
        StartCoroutine(FlashSmooth(manaOrbLoseEffect, flashDuration)); 
    }

    private void PlayManaGainEffect()
    {
        manaGainEffect.Play();
        FlashGreen();
    }

    private void PlayManaLoseEffect()
    {
        FlashRed();
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
