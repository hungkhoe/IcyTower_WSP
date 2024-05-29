using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] internal CameraController cameraController;
    [SerializeField] internal PlatformManager platformManager;
    [SerializeField] internal InGameUI inGameUI;

    public bool isPause = true;  

    private void Awake()
    {
        Instance = this;
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
}
