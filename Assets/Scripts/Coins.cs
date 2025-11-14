using UnityEngine;
using TMPro;
using System.Collections;
public class Coins : MonoBehaviour
{
    public static Coins instance { get; private set; }

    public int coinsHeld;
    [Header("UI")]
    public TextMeshProUGUI coinsHeldText;
    public GameObject coinGainTextPrefab;
    public RectTransform coinTextTransform;
    public Vector3 popupOffset;
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
        ShowCoinPopup(amount, 1); //type 1 means its adding coins
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
    
    public void ShowCoinPopup(int amount, int type) //type 1 means adding coins and type -1 means decreasing coins
    {
        GameObject popup = Instantiate(coinGainTextPrefab, coinTextTransform.parent);
        
        popup.transform.position = coinTextTransform.position + GetRandomPopupOffSet();
        
        TextMeshProUGUI tmp = popup.GetComponentInChildren<TextMeshProUGUI>();
        if (tmp != null)
        {
            if (type == 1)
            {
                tmp.text = "+" + amount;
            }
        }
        StartCoroutine(PopupFade(popup));

    }

    private Vector3 GetRandomPopupOffSet()
    {
        Vector3 offset = new Vector3(Random.Range(-15,16), Random.Range(-25, -46), Random.Range(-15, 16));
        return offset;
    }

    private IEnumerator PopupFade(GameObject popup)
    {
        CanvasGroup cg = popup.GetComponent<CanvasGroup>();
        
        float duration =.78f;
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            float progress = time / duration;
            if (cg) cg.alpha = 1f - progress;
            yield return null;
        }
    }
    
    
}
