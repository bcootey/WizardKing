using UnityEngine;

public class RotateZ : MonoBehaviour
{
    void Start()
    {
        // Keep current X and Y rotation, randomize Z
        Vector3 currentEuler = transform.eulerAngles;
        float randomZ = Random.Range(0f, 360f);
        transform.rotation = Quaternion.Euler(currentEuler.x, currentEuler.y, randomZ);
    }
}
