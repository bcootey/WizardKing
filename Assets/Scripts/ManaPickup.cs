using UnityEngine;

public class ManaPickup : MonoBehaviour
{
    public int manaGained;
    public Mana mana;
    public Transform playerTransform;
    public float speed = 2f;
    public float pickupRange = 7f;
    public GameObject manaPickupDeathParticles;
    public GameObject manaPickupParent;
    
    void Start()
    {
        mana = GameObject.FindGameObjectWithTag("Player").GetComponent<Mana>();
    }
    void Update()
    {
        playerTransform = PlayerStats.instance.playerLocation;
        float distanceToPlayer = Vector3.Distance(manaPickupParent.transform.position, playerTransform.position);

        // Move toward player if within range
        if (distanceToPlayer <= pickupRange)
        {MoveTowardsXZ
            (playerTransform.position);
        }
    }

    void MoveTowardsXZ(Vector3 destination)
    {
        Vector3 currentPosition = manaPickupParent.transform.position;
        Vector3 targetPosition = new Vector3(destination.x, currentPosition.y, destination.z);

        manaPickupParent.transform.position = Vector3.MoveTowards(currentPosition, targetPosition, speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            mana.IncreaseMana(manaGained);
            Instantiate(manaPickupDeathParticles, transform.position, Quaternion.identity);
            Destroy(manaPickupParent);
        }
    }
}