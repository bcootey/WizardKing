using UnityEngine;
using UnityEngine.EventSystems;

public class SpellHolderHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private SpellItemHolder holder;
    private SpellMenu menu;

    void Awake()
    {
        holder = GetComponent<SpellItemHolder>();
        menu = GetComponentInParent<SpellMenu>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (menu == null || menu.descriptionText == null) return;

        menu.ShowSpellDetails(holder.heldSpell);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (menu == null || menu.descriptionText == null) return;

        // Clear/hide on exit.
        menu.ClearSpellDetails();
    }
}
