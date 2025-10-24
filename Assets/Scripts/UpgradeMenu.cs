using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UpgradeMenu : MonoBehaviour
{
    [Header("Upgrade Texts")]
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI manaText;
    public TextMeshProUGUI damageText;
    public TextMeshProUGUI magicDamageText;
    public TextMeshProUGUI luckText;
    public TextMeshProUGUI skillPointText;
    
    public TextMeshProUGUI upgradeInformationText;
    public TextMeshProUGUI nextUpgradeText;
    [Header("Upgrade Buttons")]
    public GameObject[] upgradeButtons;
    [Header("references")]
    public Experience experience;

    public void UpdateText() //updates text to current values
    {
        healthText.text = PlayerStats.instance.maxHealth + "";
        manaText.text = PlayerStats.instance.maxMana + "";
        damageText.text = PlayerStats.instance.baseDamage + "";
        magicDamageText.text = PlayerStats.instance.baseMagicDamage + "";
        luckText.text = PlayerStats.instance.luck + "";
        skillPointText.text = PlayerStats.instance.skillPoints + "";
    }

    void Update()
    {
        if (PlayerStats.instance.skillPoints >= 1)
        {
            ButtonsActive();
        }
        else
        {
            ButtonsInactive();
        }
    }
    private void ButtonsActive()
    {
        for (int i = 0; i < upgradeButtons.Length; i++)
        {
            upgradeButtons[i].SetActive(true);
        }
    }

    private void ButtonsInactive()
    {
        for (int i = 0; i < upgradeButtons.Length; i++)
        {
            upgradeButtons[i].SetActive(false);
        }
    }

    public void LevelUpButton() //called from the level up button
    {
        experience.TryToLevelUp();
        UpdateText();
    }

    public void Upgrade(string stat) //called from upgrade buttons
    {
        switch (stat)
        {
            case "Health":
                PlayerStats.instance.maxHealth += 150;
                PlayerStats.instance.skillPoints -= 1;
                DisplayNextUpgradeText("HealthButton");
                UpdateText();
                break;
            case "Mana":
                PlayerStats.instance.maxMana += 75;
                PlayerStats.instance.skillPoints -= 1;
                DisplayNextUpgradeText("ManaButton");
                UpdateText();
                break;
            case "Damage":
                PlayerStats.instance.baseDamage += 3;
                PlayerStats.instance.skillPoints -= 1;
                DisplayNextUpgradeText("DamageButton");
                UpdateText();
                break;
            case "MagicDamage":
                PlayerStats.instance.baseMagicDamage += 6;
                PlayerStats.instance.skillPoints -= 1;
                DisplayNextUpgradeText("MagicDamageButton");
                UpdateText();
                break;
            case "Luck":
                PlayerStats.instance.luck += 5;
                PlayerStats.instance.skillPoints -= 1;
                DisplayNextUpgradeText("LuckButton");
                UpdateText();
                break;
            default:
                break;
        }
    }

    public void DisplayNextUpgradeText(string button)
    {
        switch (button)
        {
            //sets the information texts values to the current information plus the next upgrade information showing how much your stats will change on upgrade
            case "HealthButton":
                upgradeInformationText.text = "Increases Maximum Health";
                nextUpgradeText.text = PlayerStats.instance.maxHealth + " -> " + GetNextUpgradeValue("Health");
                break;
            case "ManaButton":
                upgradeInformationText.text = "Increases Maximum Mana";
                nextUpgradeText.text = PlayerStats.instance.maxMana + " -> " + GetNextUpgradeValue("Mana");
                break;
            case "DamageButton":
                upgradeInformationText.text = "Increases Base Melee Damage";
                nextUpgradeText.text = PlayerStats.instance.baseDamage + " -> " + GetNextUpgradeValue("Damage");
                break;
            case "MagicDamageButton":
                upgradeInformationText.text = "Increases Base Magic Damage";
                nextUpgradeText.text = PlayerStats.instance.baseMagicDamage + " -> " + GetNextUpgradeValue("MagicDamage");
                break;
            case "LuckButton":
                upgradeInformationText.text = "Increases Enemy Drop-rate";
                nextUpgradeText.text = PlayerStats.instance.luck + " -> " + GetNextUpgradeValue("Luck");
                break;
            default:
                break;
        }
    }

    private float GetNextUpgradeValue(string stat)
    {
        float nextUpgradeValue = 0;
        switch (stat)
        {
            case "Health":
                nextUpgradeValue = PlayerStats.instance.maxHealth + 150f;
                
                break;
            case "Mana":
                nextUpgradeValue = PlayerStats.instance.maxMana + 75;
                
                break;
            case "Damage":
                nextUpgradeValue = PlayerStats.instance.baseDamage + 3;
                
                break;
            case "MagicDamage":
                nextUpgradeValue = PlayerStats.instance.baseMagicDamage + 6;
               
                break;
            case "Luck":
                nextUpgradeValue = PlayerStats.instance.luck + 5;
                
                break;
            default:
                break;
        }
        
        return nextUpgradeValue;
    }

}
