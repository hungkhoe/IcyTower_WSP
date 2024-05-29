using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class InGameUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI playerScoreTxt;
    [SerializeField] private GameObject gameOverPanel;

    [SerializeField] private string mainMenuScene;
    public void UpdatePlayerScore(int _score)
    {
        playerScoreTxt.text = "Score: " + _score.ToString();
    }

    public void TurnOnGameOverUI()
    {
        gameOverPanel.SetActive(true);
    }

    public void HomeButton()
    {
        SceneManager.LoadScene(mainMenuScene);
    }

    public void PlayAgainButton()
    {
       // reset back to normal;
    }
}
