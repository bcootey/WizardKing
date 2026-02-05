using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class LoadingDoor : MonoBehaviour, IInteractable
{
    public string sceneName;
    public Vector3 newPos;

    public string GetPrompt() => "Open Door";

    public void Interact()
    {
        TravelManager.instance.TravelTo(sceneName, newPos);
    }
}
