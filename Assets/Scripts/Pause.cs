using UnityEngine;

public class Pause : MonoBehaviour
{
    public static Pause instance { get; private set; }
    public bool isPaused = false;
    public int gameObjectsCallingPause = 0;

    void Awake()
    {
        // Enforce Singleton pattern
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;

        DontDestroyOnLoad(gameObject);
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        isPaused = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        gameObjectsCallingPause++;
    }

    public void ResumeGame()
    {
        gameObjectsCallingPause--;
        if (gameObjectsCallingPause == 0)
        {
            Time.timeScale = 1f;
            isPaused = false;

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            return;
        }
    }

    public bool IsPaused()
    {
        return isPaused;
    }

}