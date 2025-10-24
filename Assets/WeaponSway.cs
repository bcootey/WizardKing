using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSway : MonoBehaviour
{
    [Header("Sway Settings")]
    public float swayAmount = 0.02f;
    public float maxSwayAmount = 0.06f;
    public float swaySmoothness = 6f;
    public float swayMultiplier = 1f;

    private Vector3 initialPosition;

    void Start()
    {
        // Save the initial position of the weapon
        initialPosition = transform.localPosition;
    }

    void Update()
    {
        // Get mouse movement input
        float mouseX = Input.GetAxis("Mouse X") * swayMultiplier;
        float mouseY = Input.GetAxis("Mouse Y") * swayMultiplier;

        // Calculate target sway position
        Vector3 targetPosition = new Vector3(-mouseX * swayAmount, -mouseY * swayAmount, 0);

        // Clamp the sway amount to prevent excessive movement
        targetPosition.x = Mathf.Clamp(targetPosition.x, -maxSwayAmount, maxSwayAmount);
        targetPosition.y = Mathf.Clamp(targetPosition.y, -maxSwayAmount, maxSwayAmount);

        // Smoothly interpolate to the target sway position
        transform.localPosition = Vector3.Lerp(transform.localPosition, initialPosition + targetPosition, Time.deltaTime * swaySmoothness);
    }

    public void SetSway(float swayAmount)
    {
        maxSwayAmount = swayAmount;
    }
}
