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
    
    public SavingMenu savingMenu;
    public PlayerInteractor playerInteractor;

    void Awake()
    {
        sceneName = this.gameObject.scene.name;
    }
    public string GetPrompt() => "Rest";

    public void Interact()
    {
        if (savingMenu == null)
            savingMenu = FindFirstObjectByType<SavingMenu>();

        if (playerInteractor == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
                playerInteractor = player.GetComponent<PlayerInteractor>();
        }

        if (savingMenu == null || playerInteractor == null)
        {
            Debug.LogError("SavePoint missing references (SavingMenu or PlayerInteractor).");
            return;
        }
        
        playerInteractor.PauseRayForSeconds(1f);
        
        if (!isUnlocked)
        {
            Unlock();
        }
        
        SpawnPointManager.instance.SetCurrentCheckpoint(locationName);
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

        //reload scene asynchronously
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