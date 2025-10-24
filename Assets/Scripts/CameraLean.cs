using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLean : MonoBehaviour
{
    public GameObject playerView;
    public float leanAngle = 5f;

    float curAngle;
    float targetAngle;
    float angle;

    float maxRot = -5.0f;
    float rate = 2.0f;

    void Update()
    {
        LeanCamera(Input.GetAxis("Horizontal"));
    }

    public void LeanCamera(float axis)
    {
        curAngle = playerView.transform.localEulerAngles.z;
        targetAngle = leanAngle - axis;

        if (axis == 0.0f) targetAngle = 0.0f;

        playerView.transform.localRotation = Quaternion.Lerp(playerView.transform.localRotation, Quaternion.Euler(playerView.transform.localRotation.x, playerView.transform.localRotation.y, axis * maxRot), Time.deltaTime * rate);

    }
}
