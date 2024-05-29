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
    private bool isGrounded;

    [SerializeField] private Transform dieCheck;
    private Camera camera;

    private void Start()
    {
        camera = Camera.main;
    }

    void Update()
    {
        if (GameManager.Instance.isPause)
            return;

        InputUpdate();     
        
        if(!isPlayerOutScreen())
        {
            GameManager.Instance.PlayerDie();
        }
    }
    private void FixedUpdate()
    {
        if (GameManager.Instance.isPause)
            return;

        MoveUpdate();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Platform")
        {       
            Vector2 collisionNormal = collision.contacts[0].normal;
           
            if (collisionNormal.y > 0.5f)
            {
                Debug.Log("Landed on top of the platform");
                isGrounded = true;
                collision.gameObject.GetComponent<Platform>().PlayerLand();
            }          
        }       
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Platform")
        {
            if (isGrounded)
                isGrounded = false;
        }
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

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
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
        _rigidBody.velocity += moveDirection * moveSpeed *Time.deltaTime;
    }
    private bool isPlayerOutScreen()
    {       
        Vector3 viewportPos = camera.WorldToViewportPoint(dieCheck.position);      
        bool isInView = viewportPos.y >= 0 && viewportPos.z > 0;
        return isInView;
    }
}
