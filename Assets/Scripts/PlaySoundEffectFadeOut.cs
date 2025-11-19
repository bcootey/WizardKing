using UnityEngine;
using System.Collections;
public class PlaySoundEffectFadeOut : MonoBehaviour
{
    public AudioSource audioSource;   // Assign in Inspector
    public float fadeDuration = 2f;   // How long the fade lasts

    void Start()
    {
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();

        if (audioSource != null)
        {
            audioSource.Play();
            StartCoroutine(FadeOut());
        }
    }

    private IEnumerator FadeOut()
    {
        float startVolume = audioSource.volume;
        float timer = 0f;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, 0f, timer / fadeDuration);
            yield return null;
        }

        audioSource.volume = 0f;
        audioSource.Stop();
    }
}
