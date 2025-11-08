using UnityEngine;
public class CoinPurse : MonoBehaviour
{
    public int coinAmount;
    public GameObject coinPursePickupDeathParticles;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Coins.instance.IncreaseCoins(coinAmount);
            Instantiate(coinPursePickupDeathParticles, transform.position, Quaternion.identity);
            Destroy(this.gameObject.transform.parent.gameObject);
        }
    }
}
