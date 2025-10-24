using UnityEngine;

public class SavePoint : MonoBehaviour, IInteractable
{
    public SavingMenu savingMenu;
    public PlayerInteractor playerInteractor;
    public string GetPrompt() => "Rest";

    public void Interact()
    {
        playerInteractor.PauseRayForSeconds(1f);
        savingMenu.EnterSavingMenu();
        
    }
}