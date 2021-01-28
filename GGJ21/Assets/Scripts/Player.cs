using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;

    [SerializeField] private float moveSpeed = 2.5f;
    [SerializeField] private float maxSpeed = 5f;
    [SerializeField] private float jumpForce = 8f;
    [SerializeField] private float lowJumpMultiplier = 2f;
    [SerializeField] private float fallMultiplier = 2.5f;
    [SerializeField] private LayerMask ground;
    [SerializeField] private float groundCheckRadius = 0.5f;
    [SerializeField] private Transform groundCheckPositionTransform;

    private Rigidbody2D playerRB;
    private Vector2 inputMovement;

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        float xMovement = Input.GetAxisRaw("Horizontal");
        inputMovement = xMovement * Vector2.right * moveSpeed;
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            playerRB.AddForce(Vector2.up * jumpForce);
        }
    }

    void FixedUpdate()
    {
        Move();
        HandleJump();
    }

    private void HandleJump()
    {
        if (playerRB.velocity.y < 0)
        {
            playerRB.velocity += Physics2D.gravity * (fallMultiplier - 1) * Time.fixedDeltaTime;
        }
        else if (playerRB.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            playerRB.velocity += Physics2D.gravity * (lowJumpMultiplier - 1) * Time.fixedDeltaTime;
        }
    }

    private void Move()
    {
        playerRB.AddForce(inputMovement);
        Vector2 vel = playerRB.velocity;
        vel.x = Mathf.Clamp(vel.x, -maxSpeed, maxSpeed);
        playerRB.velocity = vel;
    }

    bool IsGrounded()
    {
        Collider2D groundCollider = Physics2D.OverlapCircle(groundCheckPositionTransform.position, groundCheckRadius, ground);
        return groundCollider != null;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(groundCheckPositionTransform.position, groundCheckRadius);
    }
}
