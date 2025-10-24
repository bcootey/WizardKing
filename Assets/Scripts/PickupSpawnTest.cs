using UnityEngine;

public class PickupSpawnTest : MonoBehaviour
{
    [Header("Spawn Settings")]
    public GameObject prefabToSpawn;
    public Transform spawnPoint;
    public float spawnInterval = 2f;

    [Header("Force Settings")]
    public Vector3 forceDirection = new Vector3(1, 1, 0); // Customize direction
    public float forceAmount = 5f;

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnPrefab();
            timer = 0f;
        }
    }
    void SpawnPrefab()
    {
        if (prefabToSpawn == null || spawnPoint == null)
        {
            Debug.LogWarning("Missing prefab or spawn point!");
            return;
        }

        GameObject spawned = Instantiate(prefabToSpawn, spawnPoint.position, spawnPoint.rotation);

        Rigidbody rb = spawned.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(forceDirection.normalized * forceAmount, ForceMode.Impulse);
        }
    }
}
