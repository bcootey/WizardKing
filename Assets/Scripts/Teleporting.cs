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

        // Optional: fade out here

        // If target scene is different, load it
        if (SceneManager.GetActiveScene().name != data.sceneName)
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(data.sceneName);
            while (!asyncLoad.isDone)
                yield return null;
        }
        ScreenTransition.instance.StartFade(1f, 0.2f);
        yield return new WaitForSeconds(1f);
        playerTransform.position = data.position;

        Debug.Log($"Teleported player to {data.id} in scene {data.sceneName}");
    }
}

