using UnityEngine;
using System.Collections;
public class MeteorStorm : MonoBehaviour
{
    [Header("Spawn Settings")]
    public GameObject prefab;
    public int amountToSpawn = 10;
    public float radius = 10f;
    public float timeBetweenSpawns = 0.5f;

    [Header("Ground Settings")]
    public float raycastHeight = 50f;
    public LayerMask groundMask;

    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        if (prefab == null)
            yield break;

        for (int i = 0; i < amountToSpawn; i++)
        {
            SpawnOne();
            yield return new WaitForSeconds(timeBetweenSpawns);
        }
    }

    void SpawnOne()
    {
        Vector2 offset2D = Random.insideUnitCircle * radius;
        Vector3 rayStart = transform.position + new Vector3(offset2D.x, raycastHeight, offset2D.y);

        if (Physics.Raycast(rayStart, Vector3.down, out RaycastHit hit, raycastHeight * 2f, groundMask))
        {
            Instantiate(prefab, hit.point, Quaternion.identity);
        }
        else
        {
            Vector3 fallback = transform.position + new Vector3(offset2D.x, 0f, offset2D.y);
            fallback.y = transform.position.y;
            Instantiate(prefab, fallback, Quaternion.identity);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
