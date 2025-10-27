using UnityEngine;

public class SavePoint : MonoBehaviour, IInteractable
{
    public string locationName;
    public SavingMenu savingMenu;
    public PlayerInteractor playerInteractor;

    void Start()
    {
        savingMenu = GameObject.Find("SavingMenu").GetComponent<SavingMenu>();
        playerInteractor = GameObject.Find("Pyromancer").GetComponent<PlayerInteractor>();
    }
    public string GetPrompt() => "Rest";

    public void Interact()
    {
        playerInteractor.PauseRayForSeconds(1f);
        savingMenu.EnterSavingMenu(locationName);
        
    }
}