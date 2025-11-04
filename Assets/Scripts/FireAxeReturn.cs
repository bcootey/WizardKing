using UnityEngine;

public class FireAxeReturn : MonoBehaviour
{
    [Header("Movement Settings")]
    public float forwardSpeed = 25f;
    public float returnSpeed = 30f;
    public float travelTime = 0.6f;

    [Header("Raycast Settings")]
    public float rayDistance = 2f;
    public LayerMask floorLayer;

    [HideInInspector] public Transform player;

    private bool returning = false;
    private float timer = 0f;
    private bool hitFloor = false;

    void Start()
    {
        player = PlayerStats.instance.playerLocation;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (!returning)
        {
            // Move forward
            transform.position += transform.forward * forwardSpeed * Time.deltaTime;

            // Check for floor hit
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, rayDistance, floorLayer))
            {
                hitFloor = true;
                returning = true;
            }

            // Time-based return
            if (timer >= travelTime)
            {
                returning = true;
            }
        }
        else if (player != null)
        {
            // Move back to player
            Vector3 dir = (player.position - transform.position).normalized;
            transform.position += dir * returnSpeed * Time.deltaTime;

            // Destroy when close to player
            if (Vector3.Distance(transform.position, player.position) < 1.2f)
            {
                Destroy(gameObject);
            }
        }
    }

    void OnDrawGizmos()
    {
        // Display the forward raycast
        Gizmos.color = hitFloor ? Color.red : Color.green;
        Gizmos.DrawRay(transform.position, transform.forward * rayDistance);
    }
}