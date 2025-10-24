using UnityEngine;
using System.Collections;
public class Health : MonoBehaviour
{
    public static Health instance { get; private set; }
    public PlayerDash playerDash;
    public float defaultInvincibilityFrames;
    public bool canBeHit = true;
    private void Awake()
    {
        // Enforce singleton pattern
        if (instance != null && instance != this)
        {
            Destroy(gameObject); // Optional: destroy duplicates
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject); // Optional: persist across scenes
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

    private void CheckIfDead()
    {
        if (PlayerStats.instance.currentHealth <= 0)
        {
            //death logic
            Destroy(gameObject);
        }
    }
}