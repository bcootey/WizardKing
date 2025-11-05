using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    public Transform player;

    void Start()
    {
        player = PlayerStats.instance.playerLocation;
    }

    void Update()
    {
        if (player == null) return;

        // Calculate direction from enemy to player
        Vector3 direction = player.position - transform.position;

        // Optional: ignore vertical difference
        direction.y = 0;

        // Only rotate if there's a valid direction
        if (direction.sqrMagnitude > 0.001f)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 9f);
        }
    }
}
