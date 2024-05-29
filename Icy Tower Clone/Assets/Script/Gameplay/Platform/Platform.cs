using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public int platFormIndex = 0;

    private void Update()
    {
        if (GameManager.Instance.platformManager.playerHighestPlatform > platFormIndex)
        {

        }
    }

    IEnumerator FadeAwayPlatform()
    {
        yield return null;
    }

    public void PlayerLand()
    {
        GameManager.Instance.platformManager.CreatePlatform(platFormIndex);
    }
}
