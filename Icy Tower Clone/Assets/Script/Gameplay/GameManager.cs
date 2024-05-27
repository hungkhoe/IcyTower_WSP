using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] CameraController cameraController;

    private void Awake()
    {
        Instance = this;
    }

    public void IncreaseScreenSpeed()
    {
        cameraController.IncreaseScreenSpeed();
    }
}
