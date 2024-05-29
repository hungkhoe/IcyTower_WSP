using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float cameraSpeed;
    public float cameraIncreaseSpeed;

    // check if player on top screen
    public Transform player; 
    public float smoothSpeed = 0.125f;
    public float verticalOffset = 2.0f; 
    public float topScreenThreshold = 0.9f;

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.isPause)
            return;

        if (GameManager.Instance.platformManager.playerHighestPlatform >= 5)
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), cameraSpeed * Time.deltaTime);
    }

    private void LateUpdate()
    {
        CheckIfPlayerOnTopScreen();
    }

    private void CheckIfPlayerOnTopScreen()
    {
        Vector3 screenPoint = mainCamera.WorldToViewportPoint(player.position);
        if (screenPoint.y >= topScreenThreshold)
        {
            Vector3 desiredPosition = new Vector3(transform.position.x, player.position.y + verticalOffset, transform.position.z);
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
  
    public void IncreaseScreenSpeed()
    {
        cameraSpeed += cameraIncreaseSpeed;
    }
}
