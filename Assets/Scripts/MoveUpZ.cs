using UnityEngine;

public class MoveUpZ : MonoBehaviour
{
    public float maxReduction = 3f;

    void Start()
    {
        Vector3 originalScale = transform.localScale;

        // Pick a random amount to subtract (but not more than the current Z scale)
        float reduction = Random.Range(0f, Mathf.Min(maxReduction, originalScale.z));

        // Apply the reduced Z scale
        float newZ = originalScale.z - reduction;
        transform.localScale = new Vector3(originalScale.x, originalScale.y, newZ);
    }
}
