using UnityEngine;

public class NewSpellTest : MonoBehaviour, IInteractable
{
    public string GetPrompt() => "Pickup";
    public Spell spell;
    public SpellMenu spellMenu;

    public void Interact()
    {
        spellMenu.NewSpell(spell);
    }
}
