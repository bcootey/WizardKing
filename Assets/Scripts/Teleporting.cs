using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
public class Teleporting : MonoBehaviour
{
    private Transform playerTransform;
    public SavingMenu savingMenu;
    public void StartTeleport(SpawnPointData data)
    {
        StartCoroutine(TeleportRoutine(data));
    }
    private IEnumerator TeleportRoutine(SpawnPointData data)
    {
        savingMenu.ClosePopUpMenu();
        savingMenu.LeaveSaveMenu();
        playerTransform = PlayerStats.instance.transform;
        
        ScreenTransition.instance.StartFade(.75f, 1.5f);
        yield return new WaitForSeconds(1f);
        // If target scene is different, load it
        if (SceneManager.GetActiveScene().name != data.sceneName)
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(data.sceneName);
            while (!asyncLoad.isDone)
                yield return null;
        }
        playerTransform.position = data.position;

        Debug.Log($"Teleported player to {data.id} in scene {data.sceneName}");
    }
}

