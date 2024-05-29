using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool isLeft = false;
    public void OnPointerDown(PointerEventData eventData)
    {
        if (isLeft)
        {
            GameManager.Instance.playerMovement.isLeftKeyPressed = true;
        }
        else
        {
            GameManager.Instance.playerMovement.isRightkeyPressed = true;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (isLeft)
        {
            GameManager.Instance.playerMovement.isLeftKeyPressed = false;
        }
        else
        {
            GameManager.Instance.playerMovement.isRightkeyPressed = false;
        }
    }
}
