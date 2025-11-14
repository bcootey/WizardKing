using UnityEngine;

public class SkyboxRotation : MonoBehaviour
{
    [Header("Rotation speed (degrees per second)")]
    public float rotationSpeed = 1f;

    void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * rotationSpeed);
    }
}