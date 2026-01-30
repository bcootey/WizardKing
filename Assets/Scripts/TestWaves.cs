using UnityEngine;

public class TestWaves : MonoBehaviour
{
    public GameObject[] enemies;
    public float howOftenEnemiesSpawn;
    public Transform[] spawnPoints;
    private float spawnTimer;
    void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= howOftenEnemiesSpawn)
        {
            int rand = Random.Range(0, spawnPoints.Length);
            Transform spawnPoint = spawnPoints[rand];
            int rand2 = Random.Range(0, enemies.Length);
            GameObject enemy  = enemies[rand2];
            Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
            spawnTimer = 0;
        }
    }
}
