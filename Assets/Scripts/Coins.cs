using UnityEngine;
using TMPro;
public class Coins : MonoBehaviour
{
    public static Coins instance { get; private set; }

    public int coinsHeld;
    [Header("UI")]
    public TextMeshProUGUI coinsHeldText;
    public GameObject coinGainTextPrefab;
    public RectTransform coinTextTransform;
    public Vector3 popupOffset = new Vector3(0f, -40f, 0f);
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void IncreaseCoins(int amount)
    {
        coinsHeld += amount;
        UpdateCoinText();
        ShowCoinPopup(amount);
    }

    public void DecreaseCoins(int amount)
    {
        coinsHeld -= amount;
        if (coinsHeld < 0)
        {
            coinsHeld = 0;
        }
        UpdateCoinText();
    }

    public void UpdateCoinText()
    {
        coinsHeldText.text = coinsHeld.ToString();
    }
    
    public void ShowCoinPopup(int amount)
    {
        GameObject popup = Instantiate(coinGainTextPrefab, coinTextTransform.parent);
        
        popup.transform.position = coinTextTransform.position + popupOffset;
        
        TextMeshProUGUI tmp = popup.GetComponent<TextMeshProUGUI>();
        if (tmp != null)
            tmp.text = "" + amount;

    }
    
    
}
