using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlayerStats : MonoBehaviour
{
    
    //used as a getter for different scripts dont directly change values of script
    public static PlayerStats instance { get; private set; }
    [Header("Player Stats")]
    public float maxHealth;
    public float currentHealth;
    public float maxMana;
    public float currentMana;
    public int baseDamage;
    public int baseMagicDamage;
    public int luck;
    public float experience;
    [HideInInspector]
    public int skillPoints;
    [Header("Player Location")]
    public Transform playerLocation;
    [Header("Hud")]
    public Material healthWaveMaterial;
    public Material manaWaveMaterial;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI manaText;
    private void Awake()
    {
        // Singleton pattern: Ensure only one instance exists
        if (instance != null && instance != this)
        {
            Destroy(gameObject); // Optionally keep one instance
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject); // Optional: keep across scenes
    }

    public void UpdateHud()
    {
        healthWaveMaterial.SetFloat("_Health", NormalizeValue(0, maxHealth, currentHealth));
        manaWaveMaterial.SetFloat("_Health", NormalizeValue(0, maxMana, currentMana));

        healthText.text = Mathf.RoundToInt(currentHealth) + "/" + maxHealth;
        manaText.text = Mathf.RoundToInt(currentMana) + "/" + maxMana;
    }
    //gets the mana value as a number between 0 and 1 for the shader
    private float NormalizeValue(float min, float max, float number)
    {
        float normalizedNumber = (float)(number - min) / (max - min);
        //Debug.Log(normalizedNumber);
        return normalizedNumber;
    }
    private void Update()
    {
        UpdateHud();
    }
}
