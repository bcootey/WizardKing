using UnityEngine;

public class MeteorTrailTilt : MonoBehaviour
{
    public Transform trail;
    public float maxTilt = 10f;

    void Start()
    {
        Quaternion baseRotation = trail.localRotation;
        
        float tiltX = Random.Range(-maxTilt, maxTilt);
        float tiltY = Random.Range(-maxTilt, maxTilt);
        
        Quaternion tiltRotation = Quaternion.Euler(tiltX, tiltY, 0f);

        trail.localRotation = baseRotation * tiltRotation;
    }
}
