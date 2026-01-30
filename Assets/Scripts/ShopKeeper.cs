using UnityEngine;
using System.Collections;
using UnityEngine.Animations.Rigging;

public class ShopKeeper : MonoBehaviour
{
    public Animator shopKeeperAnimator;
    public bool inAnimation;
    public Transform pawTarget;
    private ShopItem currentshopItem = null;
    [Header("Sprite")]
    public SpriteRenderer shopKeeperSprite;
    [Header("Fade Settings")]
    public float fadeDuration = 0.3f;
    Coroutine fadeCoroutine;
    public IEnumerator PurchasedItem()
    {
        inAnimation = true;
        yield return StartFade(0f);
        shopKeeperAnimator.SetTrigger("Slam");
        yield return new WaitForSecondsRealtime(3f);
        yield return StartFade(1f);
        inAnimation = false;
    }

    public void StartPurchase(Transform slamPosition, ShopItem newShopItem)
    {
        pawTarget.position = slamPosition.position;
        pawTarget.rotation = slamPosition.rotation;
        currentshopItem = newShopItem;
        StartCoroutine(PurchasedItem());
    }

    public void SetAnimationFinish()
    {
        if (currentshopItem != null)
            currentshopItem.coinPrefab.SetActive(false);
    }
    IEnumerator StartFade(float targetAlpha)
    {
        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);

        fadeCoroutine = StartCoroutine(FadeSpriteAlpha(targetAlpha));
        yield return fadeCoroutine;
    }

    IEnumerator FadeSpriteAlpha(float targetAlpha)
    {
        float startAlpha = shopKeeperSprite.color.a;
        float t = 0f;

        while (t < fadeDuration)
        {
            t += Time.unscaledDeltaTime;
            float a = Mathf.Lerp(startAlpha, targetAlpha, t / fadeDuration);

            Color c = shopKeeperSprite.color;
            c.a = a;
            shopKeeperSprite.color = c;

            yield return null;
        }

        Color final = shopKeeperSprite.color;
        final.a = targetAlpha;
        shopKeeperSprite.color = final;
    }
    
}
