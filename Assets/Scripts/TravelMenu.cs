using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class TravelMenu : MonoBehaviour
{
    public SavingMenu savingMenu;
    public static TravelMenu instance;

    [Header("UI Setup")]
    [SerializeField] private Transform spawnPointListParent; // object with grid layout to organize buttons
    [SerializeField] private GameObject spawnPointButtonPrefab; // prefab for each save point button

    private Dictionary<string, GameObject> activeButtons = new Dictionary<string, GameObject>();

    private void Awake()
    {
        instance = this;
    }

    private void OnEnable()
    {
        if (SpawnPointManager.instance != null)
            SpawnPointManager.instance.SpawnPointUnlocked += OnSpawnPointUnlocked;

        RefreshUI();
    }

    private void OnDisable()
    {
        if (SpawnPointManager.instance != null)
            SpawnPointManager.instance.SpawnPointUnlocked -= OnSpawnPointUnlocked;
    }

    private void RefreshUI()
    {
        foreach (GameObject btn in activeButtons.Values)
            Destroy(btn);

        activeButtons.Clear();

        // pull IDs from SpawnPointManager
        foreach (string id in SpawnPointManager.instance.GetUnlockedIDs())
            CreateSpawnPointButton(id);
    }

    private void Start()
    {
        // Build UI from already unlocked spawn points if scene reloads
        foreach (string id in SpawnPointManager.instance.GetUnlockedIDs())
            CreateSpawnPointButton(id);
    }

    private void OnSpawnPointUnlocked(string spawnPointName)
    {
        CreateSpawnPointButton(spawnPointName);
    }

    private void CreateSpawnPointButton(string spawnPointName)
    {
        if (activeButtons.ContainsKey(spawnPointName))
            return; // avoid duplicates

        GameObject buttonObj = Instantiate(spawnPointButtonPrefab, spawnPointListParent);
        Button button = buttonObj.GetComponent<Button>();
        TextMeshProUGUI text = buttonObj.GetComponentInChildren<TextMeshProUGUI>();

        text.text = spawnPointName;

        //add click listener for teleportation
        button.onClick.AddListener(() => OnSpawnPointButtonClicked(spawnPointName));

        activeButtons.Add(spawnPointName, buttonObj);
    }

    private void OnSpawnPointButtonClicked(string spawnPointName)
    {
        Debug.Log($"Teleporting to {spawnPointName}");

        if (SpawnPointManager.instance == null)
            return;
        
        
        var data = SpawnPointManager.instance.GetSpawnPointData(spawnPointName);

        if (TravelManager.instance == null)
        {
            Debug.LogError("TravelManager is missing.");
            return;
        }
        savingMenu.ClosePopUpMenu();
        savingMenu.LeaveSaveMenu();

        TravelManager.instance.TravelTo(data.sceneName, data.position);
    }
}
