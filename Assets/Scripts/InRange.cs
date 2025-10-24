using UnityEngine;

public class InRange : MonoBehaviour
{
    [Header("Sphere Cast Settings")]
    public Transform originPoint;
    public float radius = 1f;
    public float range = 5f;
    public LayerMask detectionLayers;
    public Color gizmoColor = Color.red;

    [Tooltip("The result of the latest check — true if something is in range.")]
    public bool isInRange { get; private set; }

    private void Update()
    {
        // Constantly update the state every frame
        isInRange = CheckRange();
    }

    /// <summary>
    /// Returns the current detection state (same as isInRange).
    /// Also usable from other scripts for modular checks.
    /// </summary>
    public bool IsTargetInRange()
    {
        return isInRange;
    }

    /// <summary>
    /// Actual detection logic — private to prevent misuse.
    /// </summary>
    private bool CheckRange()
    {
        if (originPoint == null)
        {
            Debug.LogWarning("Origin Point not assigned.");
            return false;
        }

        Vector3 direction = originPoint.forward;
        return Physics.SphereCast(originPoint.position, radius, direction, out RaycastHit hit, range, detectionLayers);
    }

    private void OnDrawGizmos()
    {
        if (originPoint == null) return;

        Gizmos.color = gizmoColor;
        int segments = 10;
        for (int i = 0; i <= segments; i++)
        {
            float t = i / (float)segments;
            Vector3 pos = originPoint.position + originPoint.forward * range * t;
            Gizmos.DrawWireSphere(pos, radius);
        }
    }
}
