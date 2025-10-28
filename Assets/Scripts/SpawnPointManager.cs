using UnityEngine;
using System.Collections.Generic;
using System.Linq;
public class SpawnPointManager : MonoBehaviour
{
    public static SpawnPointManager instance;

    [Header("Unlocked Spawn Points")]
    [SerializeField] 
    private List<SpawnPointData> unlockedSpawnPoints = new List<SpawnPointData>();

    public delegate void OnSpawnPointUnlocked(string id);
    public event OnSpawnPointUnlocked SpawnPointUnlocked;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // persist between scenes
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public void AddSpawnPoint(SavePoint savePoint)
    {
        // Already unlocked?
        if (IsUnlocked(savePoint.locationName))
        {
            // Update data if needed (e.g., scene reloaded)
            var existing = unlockedSpawnPoints.FirstOrDefault(p => p.id == savePoint.locationName);
            if (existing != null)
            {
                existing.sceneName = savePoint.sceneName;
                existing.position = savePoint.teleportTransform.position;
            }
            return;
        }

        // Add new unlocked spawn point
        SpawnPointData data = new SpawnPointData(
            savePoint.locationName,
            savePoint.sceneName,
            savePoint.teleportTransform.position
        );

        unlockedSpawnPoints.Add(data);

        Debug.Log($"Unlocked spawn point: {savePoint.locationName} in scene {savePoint.sceneName}");

        // Notify listeners (like TravelMenu)
        SpawnPointUnlocked?.Invoke(savePoint.locationName);
    }
    
    public bool IsUnlocked(string id)
    {
        return unlockedSpawnPoints.Any(p => p.id == id);
    }

    public List<string> GetUnlockedIDs()
    {
        return unlockedSpawnPoints.Select(p => p.id).ToList();
    }

    /// <summary>
    /// Get full data (scene name + position) for a spawn point ID.
    /// </summary>
    public SpawnPointData GetSpawnPointData(string id)
    {
        return unlockedSpawnPoints.FirstOrDefault(p => p.id == id);
    }
}
