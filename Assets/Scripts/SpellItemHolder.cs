using UnityEngine;
using UnityEngine.UI;
public class SpellItemHolder : MonoBehaviour
{
    public Spell heldSpell;
    public SpellMenu spellMenu;
     public Image slotImage;        // the actual image displayed in the UI
    public Sprite emptySlotIcon;   // assign your "empty slot" image in the inspector
    private void Awake()
    {
        spellMenu = GetComponentInParent<SpellMenu>();
    }
    public void SetSpell(Spell spell)
    {
        heldSpell = spell;

        if (heldSpell != null && heldSpell.icon != null)
        {
            // Show the spell's icon
            slotImage.sprite = heldSpell.icon;
        }
        else
        {
            // Show default empty slot icon
            slotImage.sprite = emptySlotIcon;
        }
    }
    public void SpellButtonClicked()
    {
        if (heldSpell == null)
        {
            return;
        }
        else if (heldSpell.spellType == SpellType.Thrown)
        {
            spellMenu.SetThrownSpell(heldSpell);
        }
        else
        {
            spellMenu.SetUltimateSpell(heldSpell);
        }
    }
}
