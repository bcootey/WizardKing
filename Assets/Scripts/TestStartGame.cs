using UnityEngine;
using UnityEngine.SceneManagement;
public class TestStartGame : MonoBehaviour
{
    public void LoadScene()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
