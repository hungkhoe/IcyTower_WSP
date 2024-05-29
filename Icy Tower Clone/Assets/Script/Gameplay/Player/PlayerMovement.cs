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
    [SerializeField] private Transform wallCheck;
    [SerializeField] private float radisWallCheck;
    [SerializeField] private LayerMask wallLayer;

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

        InputUpdate();

        if (!isPlayerOutScreen())
        {
            GameManager.Instance.PlayerDie();
        }
    }
    private void FixedUpdate()
    {
        if (GameManager.Instance.isPause)
            return;

        playerVelocity = _rigidBody.velocity;

        MoveUpdate();
        CheckWall();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Platform")
        {
            Vector2 collisionNormal = collision.contacts[0].normal;

            if (collisionNormal.y > 0.5f)
            {
                Debug.Log("Landed on top of the platform");
                isGrounded = true;
                collision.gameObject.GetComponent<Platform>().PlayerLand();
                isBounce = true;
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

        if (Input.GetKey(KeyCode.RightArrow) || isRightkeyPressed)
        {
            horizontalInput = 1;
        }
        else if (Input.GetKey(KeyCode.LeftArrow) || isLeftKeyPressed)
        {
            horizontalInput = -1;
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            verticalInput = 1;
            _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, jumpForce);
            _animator.SetTrigger("jump");
        }
      
        _animator.SetFloat("vertical", _rigidBody.velocity.y);
        _animator.SetFloat("horizontal", _rigidBody.velocity.x);       
    }
    private void MoveUpdate()
    {      
        Vector2 moveDirection = new Vector2(horizontalInput, 0).normalized;
        _rigidBody.velocity += moveDirection * moveSpeed;

        if(_rigidBody.velocity.x >= playerThreadsHoldSpeedX)
        {
            _rigidBody.velocity = new Vector2(playerThreadsHoldSpeedX, _rigidBody.velocity.y);
        }

        if(_rigidBody.velocity.y >= playerThreadsHoldSpeedY)
        {
            _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, playerThreadsHoldSpeedY);
        }
    }
    private void CheckWall()
    {
        Collider2D collider2D = Physics2D.OverlapCircle(wallCheck.transform.position, radisWallCheck, wallLayer);
        if (collider2D != null && isBounce)
        {
            Vector2 bounce = new Vector2(-playerVelocity.x, playerVelocity.y);
            isBounce = false;           
            _rigidBody.velocity = bounce * bounceForce;

            _animator.SetTrigger("bounce");
        }
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
        Gizmos.DrawWireSphere(wallCheck.transform.position, radisWallCheck);
    }
}
