using UnityEngine;
using System.Collections.Generic;
public class SpawnPointManager : MonoBehaviour
{
    public static SpawnPointManager instance;
    [Header("Unlocked Spawn Point IDs")]
    public List<string> unlockedSpawnPointIDs = new List<string>();

    public delegate void OnSpawnPointUnlocked(string id);
    public event OnSpawnPointUnlocked SpawnPointUnlocked;
    
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    public void AddSpawnPoint(SavePoint savePoint)
    {
        if (!unlockedSpawnPointIDs.Contains(savePoint.locationName))
        {
            unlockedSpawnPointIDs.Add(savePoint.locationName);
            Debug.Log($"Unlocked spawn point: {savePoint.locationName}");

            SpawnPointUnlocked?.Invoke(savePoint.locationName);
        }
    }
    public bool IsUnlocked(string id) => unlockedSpawnPointIDs.Contains(id);
    
}
