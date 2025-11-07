using UnityEngine;
using TMPro;
public class SavingMenu : MonoBehaviour
{
    [Header("Menus")]
    public GameObject savingMenu;
    public GameObject upgradeMenu;
    public GameObject travelMenu;
    public GameObject spellMenu;
    private GameObject currentMenu;
    
    public TextMeshProUGUI locationText;
    
    void Start()
    {
        currentMenu = savingMenu;
    }

    public void UpgradeButton()
    {
        //closes the saving menu and opens the upgrade menu and sets it as the current menu
        savingMenu.SetActive(false);
        upgradeMenu.SetActive(true);
        currentMenu = upgradeMenu;
    }

    public void TravelButton()
    {
        savingMenu.SetActive(false);
        travelMenu.SetActive(true);
        currentMenu = travelMenu;
    }

    public void SpellButton()
    {
        savingMenu.SetActive(false);
        spellMenu.SetActive(true);
        currentMenu = spellMenu;
    }

    public void ClosePopUpMenu()
    {
        //closes the current pop up screen ex. the upgrade screen and brings it back the the default screen
        currentMenu.SetActive(false);
        currentMenu = savingMenu;
        currentMenu.SetActive(true);
    }

    public void LeaveSaveMenu()
    {
        savingMenu.SetActive(false);
        Pause.instance.ResumeGame();
    }
    public void EnterSavingMenu(string locationName) //argument that the save point passes that gives location name to menu
    {
        locationText.text = locationName;
        savingMenu.SetActive(true);
        Pause.instance.PauseGame();
    }
}
