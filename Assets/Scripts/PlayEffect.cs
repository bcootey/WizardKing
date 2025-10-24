using UnityEngine;

public class PlayEffect : MonoBehaviour
{
    public ParticleSystem effect;

    public void PlayAnimationEffect()
    {
        effect.Play();
    }

}
