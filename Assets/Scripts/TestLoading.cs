using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
public class TestLoading : MonoBehaviour, IInteractable
{
    public string GetPrompt() => "Open Door";
    public Vector3 newPos;
    public void Interact()
    {
        StartCoroutine(Teleport());

    }

    public IEnumerator Teleport()
    {
        ScreenTransition.instance.StartFade(.2f, 2f);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadSceneAsync("Stage2");
        PlayerStats.instance.playerLocation.position = newPos;
    }
    
}
