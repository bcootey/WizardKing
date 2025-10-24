using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayerObject : MonoBehaviour
{
    [Header("Target Settings")]
    public Transform player; // Assign the player in Inspector or dynamically

    [Header("Rotation Options")]
    public float rotationSpeed = 5f; // Controls how fast it rotates

    void Start()
    {
        player = PlayerStats.instance.playerLocation;
    }
    
    void Update()
    {
        if (player == null) return;

        // Full 3D look at (including pitch and tilt)
        Vector3 direction = player.position - transform.position;

        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }
}
