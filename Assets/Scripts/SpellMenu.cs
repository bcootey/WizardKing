using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
public class SpellMenu : MonoBehaviour
{
    [Header("Spell Menu")]
    public List<Spell> unlockedSpells = new List<Spell>();
    public LeftHandStateManager leftHandStateManager;
    
    public SpellItemHolder currentSpellHolder;
    public Image currentSpellHolderImage;
    public SpellItemHolder currentUltimateSpellHolder;
    public Image currentUltimateSpellHolderImage;
    
    [Header("Inventory Grid")]
    [Tooltip("Parent transform that has the GridLayoutGroup and SpellItemHolder children")]
    public Transform inventoryRoot;
    
    [Header("DescriptionText")]
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI typeText;
    public TextMeshProUGUI manaText;
    public TextMeshProUGUI damageText;

    public void Start()
    {
        UpdateCurrentSpellHolder();
        UpdateCurrentUltimateSpellHolder();
        ClearSpellDetails();
        InitializeInventoryFromUnlocked();
    }
    
    public void SetThrownSpell(Spell spell)
    {
        leftHandStateManager.currentSpell =  spell;
        UpdateCurrentSpellHolder();
    }

    public void SetUltimateSpell(Spell spell)
    {
        leftHandStateManager.currentUltimateSpell =  spell;
        UpdateCurrentUltimateSpellHolder();
    }
    public void ShowSpellDetails(Spell spell)
    {
        if (spell == null)
        {
            ClearSpellDetails();
            return;
        }

        // Name & type
        if (nameText) nameText.text = string.IsNullOrEmpty(spell.spellName) ? "Unnamed Spell" : spell.spellName;
        if (typeText) typeText.text = $"Type: {spell.spellType}";

        // Mana
        if (manaText) manaText.text = $"Mana: {spell.mana}";

        // Damage modifier (x1 means no change). Also show +/- percentage for readability.
        if (damageText)
        {
            double mod = spell.damageMod == 0 ? 1.0 : spell.damageMod;
            double pct = (mod - 1.0) * 100.0; // e.g., 1.25 -> +25%
            string pctPart = Mathf.Approximately((float)mod, 1f) ? "no modifier" : (pct >= 0 ? $"+{pct:0.#}%" : $"{pct:0.#}%");
            damageText.text = $"Damage: x{mod:0.##} ({pctPart})";
        }
        
        if (descriptionText)
        {
            descriptionText.text = string.IsNullOrEmpty(spell.description) ? "" : spell.description;
            descriptionText.gameObject.SetActive(!string.IsNullOrEmpty(descriptionText.text));
        }
    }
    
    public void ClearSpellDetails()
    {
        if (nameText) nameText.text = "";
        if (typeText) typeText.text = "";
        if (manaText) manaText.text = "";
        if (damageText) damageText.text = "";
        if (descriptionText)
        {
            descriptionText.text = "";
            descriptionText.gameObject.SetActive(false);
        }
    }
    public void NewSpell(Spell spell)
    {
        if (spell == null) return;
        
        if (unlockedSpells != null && unlockedSpells.Contains(spell)) //checks if list contains spel
            return;
        if (!AddSpellToInventory(spell))
        {
            Debug.LogWarning("[SpellMenu] Inventory full. Could not add: " + spell.spellName);
        }
    }
    public bool AddSpellToInventory(Spell spell)
    {
        if (spell == null || inventoryRoot == null) return false;

        var slots = inventoryRoot.GetComponentsInChildren<SpellItemHolder>(true);
        foreach (var slot in slots)
        {
            if (slot.heldSpell == null)
            {
                slot.SetSpell(spell);
                
                AppendToUnlocked(spell);
                return true;
            }
        }

        Debug.LogWarning("[SpellMenu] Inventory is full. Could not add: " + spell.spellName);
        return false;
    }
    private void AppendToUnlocked(Spell s)
    {
        if (s == null) return;

        if (unlockedSpells == null)
            unlockedSpells = new List<Spell>();

        if (!unlockedSpells.Contains(s))
            unlockedSpells.Add(s);
    }
    public void InitializeInventoryFromUnlocked()
    {
        if (inventoryRoot == null) return;

        var slots = inventoryRoot.GetComponentsInChildren<SpellItemHolder>(true);
        // Clear all first
        foreach (var slot in slots)
            slot.SetSpell(null);

        // Fill in order
        int i = 0;
        foreach (var s in unlockedSpells)
        {
            if (s == null) continue;
            if (i >= slots.Length) break;
            slots[i].SetSpell(s);
            i++;
        }
    }

    private void UpdateCurrentSpellHolder()
    {
        currentSpellHolder.heldSpell = leftHandStateManager.currentSpell;
        currentSpellHolderImage.sprite = currentSpellHolder.heldSpell.icon;
    }

    private void UpdateCurrentUltimateSpellHolder()
    {
        currentUltimateSpellHolder.heldSpell = leftHandStateManager.currentUltimateSpell;
        currentUltimateSpellHolderImage.sprite = currentUltimateSpellHolder.heldSpell.icon;
    }
}
