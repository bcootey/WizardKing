using UnityEngine;

public class VariableAnimSpeed : MonoBehaviour
{
    public Animator animator;
    public AnimationCurve speedCurve;

    private float timer;
    private float duration;
    private bool active;

    void Update()
    {
        if (!active) return;

        timer += Time.deltaTime;
        float t = timer / duration;

        animator.speed = speedCurve.Evaluate(t);

        if (t >= 1f)
            active = false;
    }

    // Called by animation event
    public void StartSpeedCurve()
    {
        var stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        duration = stateInfo.length;
        timer = 0f;
        active = true;
    }
}

