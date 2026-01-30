using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class LoadingDoor : MonoBehaviour, IInteractable
{
   public string sceneName;
   public string GetPrompt() => "Open Door";
   public Vector3 newPos;
   public void Interact()
   {
      StartCoroutine(Teleport());

   }

   public IEnumerator Teleport()
   {
      GameStateManager.instance.AddLoadingLock();
      ScreenTransition.instance.StartFade(.2f, 3f);
      yield return new WaitForSeconds(1f);
      SceneManager.LoadSceneAsync(sceneName);
      PlayerStats.instance.playerLocation.position = newPos;
      GameStateManager.instance.RemoveLoadingLock();
   }
}
