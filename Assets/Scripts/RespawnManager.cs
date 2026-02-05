using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnManager : MonoBehaviour
{
    public static RespawnManager instance { get; private set; }

    [Header("Player")]
    [SerializeField] GameObject playerPrefab;
    [SerializeField] string playerTag = "Player";

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
    public void RespawnAtLastCheckpoint()
    {
        StartCoroutine(RespawnRoutine());
    }

    private IEnumerator RespawnRoutine()
    {
        if (GameStateManager.instance != null)
            GameStateManager.instance.AddLoadingLock();
        
        SpawnPointData checkpointData = null;
        if (SpawnPointManager.instance != null)
            checkpointData = SpawnPointManager.instance.GetCurrentCheckpointData();
        
        string sceneToLoad;
        Vector3 spawnPosition;

        if (checkpointData != null)
        {
            sceneToLoad = checkpointData.sceneName;
            spawnPosition = checkpointData.position;
        }
        else
        {
            sceneToLoad = SceneManager.GetActiveScene().name;
            spawnPosition = Vector3.zero;
        }
        
        GameObject existingPlayer = GameObject.FindGameObjectWithTag(playerTag);
        if (existingPlayer != null)
            Destroy(existingPlayer);
        
       // if (ScreenTransition.instance != null)
          //  ScreenTransition.instance.StartFade(0.75f, 1.0f);
        
        yield return SceneManager.LoadSceneAsync(sceneToLoad);
        yield return null;
        
        Instantiate(playerPrefab, spawnPosition, Quaternion.identity);

        
        GameStateManager.instance.RemoveLoadingLock();
    }
    
}
