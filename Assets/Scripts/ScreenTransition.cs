using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ScreenTransition : MonoBehaviour
{
    public static ScreenTransition instance;
    
    private CanvasGroup canvasGroup;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0f;
    }
    public void StartFade(float fadeTime, float holdTime)
    {
        StartCoroutine(FadeRoutine(fadeTime, holdTime));
    }
    private IEnumerator FadeRoutine(float fadeTime, float holdTime)
    {
        // fade out
        float t = 0f;
        while (t < fadeTime)
        {
            t += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, t / fadeTime);
            yield return null;
        }

        canvasGroup.alpha = 1f;
        yield return new WaitForSeconds(holdTime);

        // fade in
        t = 0f;
        while (t < fadeTime)
        {
            t += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, t / fadeTime);
            yield return null;
        }

        canvasGroup.alpha = 0f;
    }
}
