using UnityEngine;
using UnityEngine.EventSystems; // Required for pointer events

public class UpgradeButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Button Identifier (e.g. HealthButton, ManaButton, etc.)")]
    public string buttonName;

    [Header("Reference to the Upgrade Menu Script")]
    public UpgradeMenu upgradeMenu;

    // Called when the mouse hovers over the button
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (upgradeMenu != null && !string.IsNullOrEmpty(buttonName))
        {
            upgradeMenu.DisplayNextUpgradeText(buttonName);
        }
    }

    // Called when the mouse stops hovering the button
    public void OnPointerExit(PointerEventData eventData)
    {
        if (upgradeMenu != null)
        {
            upgradeMenu.upgradeInformationText.text = "";
            upgradeMenu.nextUpgradeText.text = "";
        }
    }
}