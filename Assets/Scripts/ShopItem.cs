using UnityEngine;

public class ShopItem : MonoBehaviour, IInteractable
{
    public ShopKeeper currrentShopKeeper;
    public Spell currentSpellInShop;
    public bool purchased = false;
    [Header("Scroll and positions")]
    public GameObject scroll;

    public GameObject coinPrefab;
    public Transform slamPosition;
    public string GetPrompt() => "Buy";

    public void Interact()
    {
        if (currrentShopKeeper.inAnimation)
        {
            return;
        }
        if (purchased)
            return;
        if (CanBuy())
        {
            currrentShopKeeper.StartPurchase(slamPosition, this);
            scroll.SetActive(false);
            coinPrefab.SetActive(true);
            purchased = true;
            Coins.instance.DecreaseCoins(currentSpellInShop.cost);
        }
        
    }

    private bool CanBuy()
    {
        if (Coins.instance.coinsHeld >= currentSpellInShop.cost)
        {
            return true;
        }
        return false;
    }
}
