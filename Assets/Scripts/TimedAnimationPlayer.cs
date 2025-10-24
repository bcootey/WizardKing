using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedAnimationPlayer : MonoBehaviour
{
    public float StartDelay;
    public Animator animation;
    private float time = 0;
    private void Update()
    {
        time += Time.deltaTime;
        if (time >= StartDelay)
        {
            animation.SetTrigger("Start");
        }
    }
}
