using UnityEngine;

public class RaycastFloorSpawner : MonoBehaviour
{
    public GameObject prefabToSpawn;
    public float raycastDistance = 10f;
    public GameObject spellCastEffect;

    void Start()
    {
        Instantiate(spellCastEffect, this.transform.position, this.transform.rotation);
        int floorLayerMask = LayerMask.GetMask("Floor");

        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, raycastDistance, floorLayerMask))
        {
            if (prefabToSpawn != null)
            {
                // Get only the horizontal rotation (y-axis) of the spawner
                Quaternion horizontalRotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);

                // Spawn the prefab at the hit point with horizontal rotation
                Instantiate(prefabToSpawn, hit.point, horizontalRotation);
            }
            else
            {
                Debug.LogWarning("No prefab assigned to spawn.");
            }

            // Destroy this GameObject
            Destroy(gameObject);
        }
    }
}
