using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] internal CameraController cameraController;
    [SerializeField] internal PlatformManager platformManager;
    [SerializeField] internal InGameUI inGameUI;
    [SerializeField] internal PlayerMovement playerMovement;
    [SerializeField] internal ClockManager clockManager;

    public bool isPause = true;  

    private void Awake()
    {
        Instance = this;

        if(SoundManager.Instance != null)
        {
            SoundManager.Instance.SetMusic(MusicClip.InGame);
        }
    }   

    public void IncreaseScreenSpeed()
    {
        cameraController.IncreaseScreenSpeed();
    }

    public void PlayerDie()
    {
        isPause = true;
        inGameUI.TurnOnGameOverUI();
    }

    public void RestartGame()
    {
        platformManager.ResetGame();
        cameraController.ResetGame();
        playerMovement.ResetGame();
        inGameUI.TurnOnBoardPanel();
        clockManager.ResetGame();
    }
}
