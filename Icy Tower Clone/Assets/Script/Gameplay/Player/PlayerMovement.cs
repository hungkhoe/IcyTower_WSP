using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;

    [SerializeField] private Rigidbody2D _rigidBody;
    [SerializeField] private Animator _animator;   

    private float horizontalInput;
    private float verticalInput;   

    void Update()
    {
        if (GameManager.Instance.isPause)
            return;

        InputUpdate();     
    }
    private void FixedUpdate()
    {
        if (GameManager.Instance.isPause)
            return;

        MoveUpdate();
    }

    private void InputUpdate()
    {
        horizontalInput = 0;
        verticalInput = 0;

        if (Input.GetKey(KeyCode.RightArrow))
        {
            horizontalInput = 1;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            horizontalInput = -1;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            verticalInput = 1;
            _rigidBody.AddForce(new Vector2(0, jumpForce));
        }
      
        _animator.SetFloat("vertical", verticalInput);
        _animator.SetFloat("horizontal", horizontalInput);    
    }
    private void MoveUpdate()
    {           
        Vector2 moveDirection = new Vector2(horizontalInput, 0);
        _rigidBody.AddForce(moveDirection * moveSpeed,ForceMode2D.Force);
    }  
}
