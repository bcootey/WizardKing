using UnityEngine;

public class PlaySoundEffect : MonoBehaviour
{
    public AudioSource audioSource;
    public void PlaySound()
    {
        audioSource.Play();
    }
}
