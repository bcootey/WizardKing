using UnityEngine;

public class TextureScroller : MonoBehaviour
{
    [SerializeField] private Vector2 scrollSpeed = new Vector2(0.5f, 0f);
    private Renderer objectRenderer;
    private Vector2 currentOffset = Vector2.zero;

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        if (objectRenderer == null)
        {
            Debug.LogWarning("TextureScroller: No Renderer found on this GameObject.");
        }
    }

    void Update()
    {
        if (objectRenderer != null)
        {
            currentOffset += scrollSpeed * Time.deltaTime;
            objectRenderer.material.mainTextureOffset = currentOffset;
        }
    }
}
