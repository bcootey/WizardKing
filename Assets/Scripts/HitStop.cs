using System.Collections;
using UnityEngine;

public class HitStop : MonoBehaviour
{
    public static HitStop instance { get; private set; }

    bool waiting;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        
    }

    public void Stop(float duration)
    {
        if (waiting) return;
        if (GameStateManager.instance != null &&
            GameStateManager.instance.State != GameState.Playing) return;

        Time.timeScale = 0.35f;
        StartCoroutine(Wait(duration));
    }

    IEnumerator Wait(float duration)
    {
        waiting = true;
        yield return new WaitForSecondsRealtime(duration);

        if (GameStateManager.instance == null ||
            GameStateManager.instance.State == GameState.Playing)
        {
            Time.timeScale = 1f;
        }

        waiting = false;
    }
}