using System.Collections;
using UnityEngine;

public class UnblockableIndicator : MonoBehaviour
{
    [SerializeField] Renderer renderer;
    public float fadeInDuration = 0.3f;
    public float fadeOutDuration = 0.3f;

    static readonly int BaseColor = Shader.PropertyToID("_BaseColor");
    static readonly int ColorProp = Shader.PropertyToID("_Color");

    MaterialPropertyBlock mpb;
    int prop;
    Color baseColor;
    Coroutine co;

    void Awake()
    {
        renderer ??= GetComponent<Renderer>();
        mpb = new MaterialPropertyBlock();

        var mat = renderer.sharedMaterial;
        prop = (mat != null && mat.HasProperty(BaseColor)) ? BaseColor : ColorProp;
        baseColor = (mat != null && mat.HasProperty(prop)) ? mat.GetColor(prop) : Color.white;

        SetAlpha(0f);
    }
    
    public void Flash()
    {
        Flash(fadeInDuration, fadeOutDuration);
    }
    
    public void Flash(float fadeIn, float fadeOut)
    {
        if (co != null) StopCoroutine(co);
        co = StartCoroutine(FlashCoroutine(fadeIn, fadeOut));
    }

    IEnumerator FlashCoroutine(float fadeIn, float fadeOut)
    {
        if (fadeIn > 0f) yield return Fade(0f, 1f, fadeIn);
        else SetAlpha(1f);

        if (fadeOut > 0f) yield return Fade(1f, 0f, fadeOut);
        else SetAlpha(0f);
    }

    IEnumerator Fade(float from, float to, float time)
    {
        for (float t = 0f; t < time; t += Time.unscaledDeltaTime)
        {
            float u = Mathf.SmoothStep(0f, 1f, t / time);
            SetAlpha(Mathf.Lerp(from, to, u));
            yield return null;
        }
        SetAlpha(to);
    }

    void SetAlpha(float a)
    {
        renderer.GetPropertyBlock(mpb);
        var c = baseColor; c.a = a;
        mpb.SetColor(prop, c);
        renderer.SetPropertyBlock(mpb);
    }
}