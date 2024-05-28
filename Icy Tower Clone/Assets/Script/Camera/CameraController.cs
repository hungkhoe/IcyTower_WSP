using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float cameraSpeed;
    public float cameraIncreaseSpeed = 0.05f;

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.isPause)
            return;

        transform.Translate(0, cameraSpeed * Time.deltaTime, 0);
    }

    public void IncreaseScreenSpeed()
    {
        cameraSpeed += cameraIncreaseSpeed;
    }
}
