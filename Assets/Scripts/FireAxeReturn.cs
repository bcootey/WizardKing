using UnityEngine;

public class FireAxeReturn : MonoBehaviour
{
    public float forwardSpeed = 25f;
    public float returnSpeed = 30f;
    public float travelTime = 0.6f;

    [HideInInspector] public Transform player;

    private bool returning = false;
    private float timer = 0f;

    void Start()
    {
        player = PlayerStats.instance.playerLocation;
    }
    void Update()
    {
        timer += Time.deltaTime;

        if (!returning)
        {
            transform.position += transform.forward * forwardSpeed * Time.deltaTime;
            
            if (timer >= travelTime)
            {
                returning = true;
            }
        }
        else if (player != null)
        {
            Vector3 dir = (player.position - transform.position).normalized;
            transform.position += dir * returnSpeed * Time.deltaTime;
            
            if (Vector3.Distance(transform.position, player.position) < 1.2f)
            {
                Destroy(gameObject);
            }
        }
    }
}