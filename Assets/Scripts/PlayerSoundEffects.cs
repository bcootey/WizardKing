using UnityEngine;

public class PlayerSoundEffects : MonoBehaviour
{
    public AudioSource swingSound;

    public void PlaySwingSound()
    {
        swingSound.Play();
    }
}
