using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TravelManager : MonoBehaviour
{
    public static TravelManager instance { get; private set; }

    [Header("Settings")]
    [SerializeField] string playerTag = "Player";

    //where the player should be placed after the next scene loads
    private bool hasPendingSpawn = false;
    private Vector3 pendingSpawnPos;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public void TravelTo(string targetSceneName, Vector3 spawnPosition)
    {
        StartCoroutine(TravelRoutine(targetSceneName, spawnPosition));
    }
    private IEnumerator TravelRoutine(string targetSceneName, Vector3 spawnPosition)
    {
        if (GameStateManager.instance != null)
            GameStateManager.instance.AddLoadingLock();
        
        hasPendingSpawn = true;
        pendingSpawnPos = spawnPosition;
        
        if (ScreenTransition.instance != null)
            ScreenTransition.instance.StartFade(0.2f, 3f);

        yield return new WaitForSeconds(1f);
        
        AsyncOperation load = SceneManager.LoadSceneAsync(targetSceneName);
        while (!load.isDone)
            yield return null;

        //wait one more frame so the player in the new scene exists
        yield return null;

        PlacePlayerIfNeeded();

        GameStateManager.instance.ClearAllLocks();
    }
    private void PlacePlayerIfNeeded()
    {
        if (!hasPendingSpawn) return;

        GameObject player = GameObject.FindGameObjectWithTag(playerTag);
        if (player == null)
        {
            Debug.LogError($"TravelManager: Could not find Player with tag '{playerTag}' after scene load.");
            return;
        }

        player.transform.position = pendingSpawnPos;
        hasPendingSpawn = false;
    }
}
