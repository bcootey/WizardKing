using UnityEngine;
using UnityEngine.SceneManagement;
public class TestLoading : MonoBehaviour, IInteractable
{
    public string GetPrompt() => "Open Door";
    public Vector3 newPos;
    public void Interact()
    {
        PlayerStats.instance.playerLocation.position = newPos;
        SceneManager.LoadSceneAsync("Stage2");
        ScreenTransition.instance.StartFade(.2f, 2f);
        
    }
}
