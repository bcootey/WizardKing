using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TestingMenu : MonoBehaviour
{
    public Spell[] spellList;
    public LeftHandStateManager leftHandStateManager;
    public Image testingMenu;

    public TextMeshProUGUI healthText;
    public TextMeshProUGUI manaText;
    public TextMeshProUGUI baseDamageText;
    public TextMeshProUGUI baseMagicDamageText;

    public TMP_InputField maxHealthInputField;
    public TMP_InputField maxManaInputField;
    public TMP_InputField baseDamageInputField;
    public TMP_InputField baseMagicDamageInputField;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            testingMenu.gameObject.SetActive(true);
            Pause.instance.PauseGame();
        }
        healthText.text = "Max Health: " + PlayerStats.instance.maxHealth;
        baseDamageText.text = "Base Damage: " + PlayerStats.instance.baseDamage;
        baseMagicDamageText.text = "Base Magic Damage: " + PlayerStats.instance.baseMagicDamage;
    }
    public void SelectSpell(int spellId)
    {
        leftHandStateManager.currentSpell = spellList[spellId];
    }
    public void CloseMenu()
    {
        testingMenu.gameObject.SetActive(false);
        Pause.instance.ResumeGame();
    }

    public void ChangeMaxHealth()
    {
        string input = maxHealthInputField.text;

        if (int.TryParse(input, out int number))
        {
            PlayerStats.instance.maxHealth += number;
            Debug.Log("Max Health changed by: " + number);
        }
        else
        {
            Debug.Log("Invalid Max Health input");
        }
    }
    public void ChangeMaxMana()
    {
        string input = maxManaInputField.text;

        if (int.TryParse(input, out int number))
        {
            PlayerStats.instance.maxMana += number;
            Debug.Log("Max Mana changed by: " + number);
        }
        else
        {
            Debug.Log("Invalid Max Mana input");
        }
    }

    public void ChangeBaseDamage()
    {
        string input = baseDamageInputField.text;

        if (int.TryParse(input, out int number))
        {
            PlayerStats.instance.baseDamage += number;
            Debug.Log("Base Damage changed by: " + number);
        }
        else
        {
            Debug.Log("Invalid Base Damage input");
        }
    }

    public void ChangeBaseMagicDamage()
    {
        string input = baseMagicDamageInputField.text;

        if (int.TryParse(input, out int number))
        {
            PlayerStats.instance.baseMagicDamage += number;
            Debug.Log("Base Magic Damage changed by: " + number);
        }
        else
        {
            Debug.Log("Invalid Base Magic Damage input");
        }
    }

}
