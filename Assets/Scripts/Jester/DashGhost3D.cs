using System.Collections.Generic;
using UnityEngine;

public class DashGhost3D : MonoBehaviour
{
    [Header("Fade Settings")]
    public float lifetime = 0.3f;
    public float startAlpha = 0.7f;

    private float timer;
    private readonly List<Material> materials = new List<Material>();

    void Awake()
    {
        foreach (var rend in GetComponentsInChildren<Renderer>())
        {
            if (rend == null) continue;
            
            var mat = rend.material;
            if (mat != null)
            {
                if (mat.HasProperty("_Color"))
                {
                    var c = mat.color;
                    c.a = startAlpha;
                    mat.color = c;
                }

                materials.Add(mat);
            }
        }
    }

    void Update()
    {
        timer += Time.deltaTime;
        float t = timer / lifetime;

        float alpha = Mathf.Lerp(startAlpha, 0f, t);
        SetAlpha(alpha);

        if (timer >= lifetime)
        {
            Destroy(gameObject);
        }
    }

    private void SetAlpha(float alpha)
    {
        foreach (var mat in materials)
        {
            if (mat == null) continue;
            if (!mat.HasProperty("_Color")) continue;

            var c = mat.color;
            c.a = alpha;
            mat.color = c;
        }
    }
}