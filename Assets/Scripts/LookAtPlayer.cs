using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    public Transform player;
    public bool isLooking = true;

    void Start()
    {
        player = PlayerStats.instance.playerLocation;
    }

    void Update()
    {
        if (isLooking)
        {
            Vector3 direction = player.position - transform.position;
        
            direction.y = 0;
        
            if (direction.sqrMagnitude > 0.001f)
            {
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 9f);
            }
        }
    }
}
