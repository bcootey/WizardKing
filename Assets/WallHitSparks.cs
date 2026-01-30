using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallHitSparks : MonoBehaviour
{
    public float rayLength = 5f;              
    public LayerMask wallLayer;             
    public GameObject prefabToInstantiate;    
    
    void Update()
    {
        Vector3 rayDirection = transform.forward;
        
        RaycastHit hit;
        if (Physics.Raycast(transform.position, rayDirection, out hit, rayLength, wallLayer))
        {

            Instantiate(prefabToInstantiate, hit.point, Quaternion.identity);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Vector3 rayDirection = transform.forward;
        Gizmos.DrawRay(transform.position, rayDirection * rayLength);
    }
}
