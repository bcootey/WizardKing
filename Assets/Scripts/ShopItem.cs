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
    private SpellMenu spellMenu;
    private string interactText = "Buy";
    void Start()
    {
        spellMenu = FindObjectOfType<SpellMenu>(true);
    }

    public string GetPrompt()
    {
        return interactText;
    }

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
            spellMenu.AddSpellToInventory(currentSpellInShop);
            Debug.Log("Bought " + currentSpellInShop.name);
            interactText = "No Longer Available"; //changes interact text to blank so it doesnt appear
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
