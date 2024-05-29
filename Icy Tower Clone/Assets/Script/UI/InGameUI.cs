using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InGameUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI playerScoreTxt;  
    public void UpdatePlayerScore(int _score)
    {
        playerScoreTxt.text = "Score: " + _score.ToString();
    }
}
