using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healthGained;
    public Health health;
    public Transform playerTransform;
    public float speed = 2f;
    public float pickupRange = 7f;
    public GameObject healthPickupDeathParticles;
    public GameObject healthPickupParent;

    void Start()
    {
        health = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
    }
    void Update()
    {
        playerTransform = PlayerStats.instance.playerLocation;
        float distanceToPlayer = Vector3.Distance(healthPickupParent.transform.position, playerTransform.position);
        transform.Rotate(Vector3.forward * 125 * Time.deltaTime);
        // Move toward player if within range
        if (distanceToPlayer <= pickupRange)
        {
            MoveTowardsXZ(playerTransform.position);
        }
    }

    void MoveTowardsXZ(Vector3 destination)
    {
        Vector3 currentPosition = healthPickupParent.transform.position;
        Vector3 targetPosition = new Vector3(destination.x, currentPosition.y, destination.z);

        healthPickupParent.transform.position = Vector3.MoveTowards(currentPosition, targetPosition, speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            health.IncreaseHealth(healthGained);
            Instantiate(healthPickupDeathParticles, transform.position, Quaternion.identity);
            Destroy(healthPickupParent);
        }
    }
}
