using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallHitSparks : MonoBehaviour
{
    public float rayLength = 5f;              // Length of the ray
    public LayerMask wallLayer;               // Layer mask for the Wall layer
    public GameObject prefabToInstantiate;    // The prefab to instantiate

    // Update is called once per frame
    void Update()
    {
        // Define the ray direction (forward of the object)
        Vector3 rayDirection = transform.forward;

        // Perform the raycast
        RaycastHit hit;
        if (Physics.Raycast(transform.position, rayDirection, out hit, rayLength, wallLayer))
        {
            // Check if the object hit is on the Wall layer and instantiate the prefab at the hit point
            Instantiate(prefabToInstantiate, hit.point, Quaternion.identity);
        }
    }

    // This function draws the ray in the Scene view for visualization
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        // Define the ray direction (forward of the object)
        Vector3 rayDirection = transform.forward;

        // Draw the ray
        Gizmos.DrawRay(transform.position, rayDirection * rayLength);
    }
}
