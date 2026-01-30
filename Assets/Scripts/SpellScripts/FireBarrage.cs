using UnityEngine;

public class FireBarrage : MonoBehaviour
{
    public GameObject fireBarrageShards;
    public Transform forwardShot;
    public Transform leftShot;
    public Transform rightShot;
    public float spawnDelay;
    public float shardSpeed;

    void Start()
    {
        Invoke("SpawnFireBarrage", spawnDelay);
    }

    private void SpawnFireBarrage()
    {
        SpawnShards(forwardShot);
        SpawnShards(leftShot);
        SpawnShards(rightShot);
    }

    private void SpawnShards(Transform direction)
    {
        Vector3 shootingDirection = direction.forward;
        GameObject shard = Instantiate(fireBarrageShards, forwardShot.position, direction.rotation);
        Rigidbody rb = shard.GetComponent<Rigidbody>();
        rb.AddForce(shootingDirection * shardSpeed, ForceMode.Impulse);
    }
}
