using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] CameraController cameraController;

    public bool isPause = true;

    private void Awake()
    {
        Instance = this;
    }

    public void IncreaseScreenSpeed()
    {
        cameraController.IncreaseScreenSpeed();
    }
}
