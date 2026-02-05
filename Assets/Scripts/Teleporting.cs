using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
public class Teleporting : MonoBehaviour
{
    [Header("Refs")]
    public SavingMenu savingMenu;

    [Header("Player")]
    [SerializeField] GameObject playerPrefab;
    [SerializeField] string playerTag = "Player";
    public void StartTeleport(SpawnPointData data)
    {
        StartCoroutine(TeleportRoutine(data));
    }
    private IEnumerator TeleportRoutine(SpawnPointData data)
    {
        if (GameStateManager.instance != null)
            GameStateManager.instance.AddLoadingLock();

        if (savingMenu != null)
        {
            savingMenu.ClosePopUpMenu();
            savingMenu.LeaveSaveMenu();
        }

        if (ScreenTransition.instance != null)
            ScreenTransition.instance.StartFade(0.75f, 1.5f);

        yield return new WaitForSeconds(1f);

        // 1) Load scene if needed
        if (SceneManager.GetActiveScene().name != data.sceneName)
        {
            AsyncOperation load = SceneManager.LoadSceneAsync(data.sceneName);
            while (!load.isDone)
                yield return null;

            yield return null;
        }
        
        GameObject player = GameObject.FindGameObjectWithTag(playerTag);

        if (player == null)
        {
            player = Instantiate(playerPrefab);
            yield return null;
        }
        
        player.transform.position = data.position;
        
        CameraFix();

        Debug.Log($"Teleported player to {data.id} in scene {data.sceneName}");

        if (GameStateManager.instance != null)
            GameStateManager.instance.RemoveLoadingLock();
    }

    private void CameraFix()
    {
        var cam = Camera.main;
        if (cam != null)
        {
            var psx = cam.GetComponent<PSXEffects>();
            if (psx != null)
            {
                psx.enabled = false;
                psx.enabled = true;
            }

           
        }
    }
}

