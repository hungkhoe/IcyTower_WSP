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
    [SerializeField] private GameObject onBoardPanel;

    [SerializeField] private string mainMenuScene;
    public void UpdatePlayerScore(int _score)
    {
        playerScoreTxt.text = "Score: " + _score.ToString();
    }

    public void TurnOnGameOverUI()
    {
        gameOverPanel.SetActive(true);
    }

    private void TurnOffGameOverUI()
    {
        gameOverPanel.SetActive(false);
    }

    public void TurnOnBoardPanel()
    {
        onBoardPanel.SetActive(true);
    }

    public void HomeButton()
    {
        SceneManager.LoadScene(mainMenuScene);

        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.SetMusic(MusicClip.Home);
        }
    }

    public void PlayAgainButton()
    {
        // reset back to normal;
        UpdatePlayerScore(0);
        TurnOffGameOverUI();
        GameManager.Instance.RestartGame();
    }
}
