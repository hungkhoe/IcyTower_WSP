using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    public float bounceForce;

    public float playerThreadsHoldSpeedX;
    public float playerThreadsHoldSpeedY;

    [SerializeField] private Rigidbody2D _rigidBody;
    [SerializeField] private Animator _animator;

    private float horizontalInput;
    private float verticalInput;

    private bool isGrounded;   
    private bool isBounce = false;

    [SerializeField] private Transform dieCheck;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private LayerMask groundMask;

    private Camera camera;

    private Vector3 startPos;
    private Vector2 playerVelocity;

    internal bool isLeftKeyPressed;
    internal bool isRightkeyPressed;

    private void Start()
    {
        camera = Camera.main;
        startPos = transform.position;
    }

    void Update()
    {
        if (GameManager.Instance.isPause)
            return;

        if (!isPlayerOutScreen())
        {
            GameManager.Instance.PlayerDie();
        }

        InputUpdate();      
    }
    private void FixedUpdate()
    {
        if (GameManager.Instance.isPause)
            return;

        GroundCheck();
        MoveUpdate();       

        playerVelocity = _rigidBody.velocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Platform")
        {
            Vector2 collisionNormal = collision.contacts[0].normal;

            if (collisionNormal.y > 0.5f)
            {                          
                collision.gameObject.GetComponent<Platform>().PlayerLand();              
            }
        }

        if (collision.collider.tag == "Wall")
        {          
            if ((isGrounded && Mathf.Abs(playerVelocity.x) < 4.54f) /*||(!isGrounded && Mathf.Abs(playerVelocity.x) < 10f)*/) // if force too small = cancel bounce
                return;

            Vector2 bounce = Vector2.one;
            
            if (isGrounded)
            {
                bounce = new Vector2(-playerVelocity.x, playerVelocity.y * 1.25f);
            }
            else
            {
                bounce = new Vector2(-playerVelocity.x, playerVelocity.y);
            }

            isBounce = true;      
            _rigidBody.velocity = bounce * bounceForce;            
            Invoke("FinishBounce", 0.05f); 
        }
    }      

    private void InputUpdate()
    {
        horizontalInput = 0;
        verticalInput = 0;

        if (isBounce == false)
        {        
            if (Input.GetKey(KeyCode.RightArrow) || isRightkeyPressed)
            {
                if (_rigidBody.velocity.x < 0 ) // cancel speed of opposite strafe
                {
                    if (isGrounded)
                        _rigidBody.velocity = new Vector2(0, _rigidBody.velocity.y);
                    else
                        _rigidBody.velocity = new Vector2(-2, _rigidBody.velocity.y);
                }
                horizontalInput = 1;
            }
            else if (Input.GetKey(KeyCode.LeftArrow) || isLeftKeyPressed)
            {
                if (_rigidBody.velocity.x > 0) // cancel speed of opposite strafe
                {
                    if (isGrounded)
                        _rigidBody.velocity = new Vector2(0, _rigidBody.velocity.y);
                    else
                        _rigidBody.velocity = new Vector2(2, _rigidBody.velocity.y);
                }
                horizontalInput = -1;
            }

            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {                   
                verticalInput = 1;              
                _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, jumpForce + (Mathf.Abs(_rigidBody.velocity.x)));
                _animator.SetTrigger("jump");             
            }
        }

        _animator.SetFloat("horizontal", _rigidBody.velocity.x);
        _animator.SetFloat("vertical", _rigidBody.velocity.y);         
        _animator.SetBool("isGround", isGrounded);         
    }
    private void MoveUpdate()
    {      
        Vector2 moveDirection = new Vector2(horizontalInput, 0).normalized;

        if (isGrounded)
            _rigidBody.velocity += moveDirection * moveSpeed;
        else
        {
            if(horizontalInput == -1 )
            {
                if (_rigidBody.velocity.x > 0)
                    _rigidBody.velocity += moveDirection * 5;
                else
                    _rigidBody.velocity += moveDirection * moveSpeed;
            }
            else if(horizontalInput == 1)
            {
                if (_rigidBody.velocity.x < 0)
                    _rigidBody.velocity += moveDirection * 5;
                else
                    _rigidBody.velocity += moveDirection * moveSpeed;
            }
        }


        if(_rigidBody.velocity.x >= playerThreadsHoldSpeedX)
        {
            _rigidBody.velocity = new Vector2(playerThreadsHoldSpeedX, _rigidBody.velocity.y);
        }
        else if(_rigidBody.velocity.x <= -playerThreadsHoldSpeedX)
        {
            _rigidBody.velocity = new Vector2(-playerThreadsHoldSpeedX, _rigidBody.velocity.y);
        }

        if(_rigidBody.velocity.y >= playerThreadsHoldSpeedY)
        {
            _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, playerThreadsHoldSpeedY);
        }
    }    
    private void GroundCheck()
    {
        Collider2D hit = Physics2D.OverlapCircle(groundCheck.transform.position, groundCheckRadius,groundMask);
        if(hit == null)
        {
            isGrounded = false;
        }
        else
        {
            isGrounded = true;
        }
    }

    private void FinishBounce()
    {
        isBounce = false;
    }
    private bool isPlayerOutScreen()
    {       
        Vector3 viewportPos = camera.WorldToViewportPoint(dieCheck.position);      
        bool isInView = viewportPos.y >= 0 && viewportPos.z > 0;
        return isInView;
    }


    public void ResetGame()
    {
        transform.position = startPos;
    }    

    public void JumpButton()
    {
        if (isGrounded)
        {
            verticalInput = 1;
            _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, jumpForce);
            _animator.SetTrigger("jump");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.transform.position, groundCheckRadius);
    }
}
