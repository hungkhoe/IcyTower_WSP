using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] private SpriteRenderer platformSprite;

    public int platFormIndex = 0;
    private bool isFading = false;

    private void Update()
    {       
        if (isFading == true)
            return;

        if (platFormIndex <= GameManager.Instance.platformManager.playerHighestPlatform && GameManager.Instance.platformManager.playerHighestPlatform > 5)
        {
            if(platFormIndex <= GameManager.Instance.platformManager.currentPlatformDisapear)
            {
                StartCoroutine(FadeAwayPlatform());
            }          
        }
    }

    IEnumerator FadeAwayPlatform()
    {
        isFading = true;

        while (platformSprite.color.a > 0)
        {
            platformSprite.color = new Color(platformSprite.color.r, platformSprite.color.g, platformSprite.color.b, platformSprite.color.a - 0.65f * Time.deltaTime);
            yield return null;
        }

        yield return new WaitForSeconds(0.3f);

        GameManager.Instance.platformManager.currentPlatformDisapear = platFormIndex + 1;

        if (platFormIndex != 0)
            SimplePool.Despawn(gameObject);
        else
            gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        platformSprite.color = new Color(platformSprite.color.r, platformSprite.color.g, platformSprite.color.b, 1);
        isFading = false;
    }

    public void PlayerLand()
    {
        GameManager.Instance.platformManager.CreatePlatform(platFormIndex);
    }
}
