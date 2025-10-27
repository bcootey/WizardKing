using UnityEngine;

public class SavePoint : MonoBehaviour, IInteractable
{
    [Header("Save point settings")]
    public string locationName;
    public bool isUnlocked = false;
    
    private SavingMenu savingMenu;
    private PlayerInteractor playerInteractor;

    void Start()
    {
        savingMenu = GameObject.Find("SavingMenu").GetComponent<SavingMenu>();
        playerInteractor = GameObject.Find("Pyromancer").GetComponent<PlayerInteractor>();
        
        // Ask the manager if this one was already unlocked (from a previous scene)
        if (SpawnPointManager.instance != null)
        {
            isUnlocked = SpawnPointManager.instance.IsUnlocked(locationName);
        }
    }
    public string GetPrompt() => "Rest";

    public void Interact()
    {
        playerInteractor.PauseRayForSeconds(1f);
        
        if (!isUnlocked)
        {
            Unlock();
        }
        
        savingMenu.EnterSavingMenu(locationName);
        
    }
    void Unlock()
    {
        isUnlocked = true;
        SpawnPointManager.instance.AddSpawnPoint(this);
    }
}