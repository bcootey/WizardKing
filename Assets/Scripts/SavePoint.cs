using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class SavePoint : MonoBehaviour, IInteractable
{
    [Header("Save point settings")]
    public string locationName;
    [HideInInspector]
    public string sceneName;
    public bool isUnlocked = false;
    public Transform teleportTransform;
    
    private SavingMenu savingMenu;
    private PlayerInteractor playerInteractor;

    void Awake()
    {
        sceneName = this.gameObject.scene.name;
    }
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
        RegainHealthAndMana();
        
    }
    void Unlock()
    {
        isUnlocked = true;
        SpawnPointManager.instance.AddSpawnPoint(this);
    }
    public void ReloadCurrentScene()
    {
        StartCoroutine(ReloadRoutine());
    }

    private IEnumerator ReloadRoutine()
    {
        if (ScreenTransition.instance != null)
            ScreenTransition.instance.StartFade(0.75f, 1.0f);

        yield return new WaitForSeconds(1f);

        string currentScene = SceneManager.GetActiveScene().name;

        // Reload scene asynchronously
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(currentScene);
        while (!asyncLoad.isDone)
            yield return null;

        Debug.Log($"Reloaded scene '{currentScene}' at save point '{locationName}'");
    }

    private void RegainHealthAndMana()
    {
        Health health = Health.instance;
        Mana mana = Mana.instance;
        health.IncreaseHealth((int)PlayerStats.instance.maxHealth);
        mana.IncreaseMana((int)PlayerStats.instance.maxMana);
    }
}