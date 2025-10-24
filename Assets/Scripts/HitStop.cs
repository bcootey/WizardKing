using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class HitStop : MonoBehaviour
{
    // Singleton instance
    public static HitStop instance { get; private set; }

    bool waiting;

    private void Awake()
    {
        // Singleton enforcement
        if (instance != null && instance != this)
        {
            Destroy(gameObject); // Prevent duplicates
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject); // Optional: persist across scenes
    }

    public void Stop(float duration)
    {
        if (waiting)
        {
            return;
        }

        Time.timeScale = 0.35f;
        StartCoroutine(Wait(duration));
    }

    IEnumerator Wait(float duration)
    {
        waiting = true;

        // Use unscaled time so hitstop isn't affected by Time.timeScale = 0
        yield return new WaitForSecondsRealtime(duration);

        Time.timeScale = 1.0f;
        waiting = false;
    }
}
