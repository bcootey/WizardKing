using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestParticles : MonoBehaviour
{
    public ParticleSystem particle1;
    public ParticleSystem particle2;
    public void PlayParticle1()
    {
        particle1.Play();
    }
    public void PlayParticle2()
    {
        particle2.Play();
    }
}
