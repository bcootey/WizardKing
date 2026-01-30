using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    [Tooltip("Prefab to instantiate when colliding with a wall")]
    public GameObject prefabToSpawn;
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Wall" || other.tag == "Enemy")
        {
            Debug.Log("Triggered with wall!");

            if (prefabToSpawn != null)
            {
                Instantiate(prefabToSpawn, transform.position, Quaternion.identity);
                Destroy(this.gameObject);
            }
        }
    }
}
