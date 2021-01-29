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
    [SerializeField] private float initialSanity = 100f;
    [SerializeField] private float sanityDegrationSpeed = 5f;
    [SerializeField] private PhysicsMaterial2D slipperyMaterial2D;
    [SerializeField] private PhysicsMaterial2D fullFrictionMaterial2D;

    private Rigidbody2D playerRB;
    private Vector2 inputMovement;
    private Vector2 groundAngleVector;
    private Collider2D playerCollider;
    public float currentSanity;
    private bool isGhost;
    private SpriteRenderer playerSprite;

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentSanity = initialSanity;
        playerRB = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>();
        playerSprite = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
        HandleSlopes();

        if(Input.GetKeyDown(KeyCode.F))
        {
            currentSanity -= 9;
            Debug.Log(currentSanity);
            if (currentSanity <= 40)
            {
                BlackBoard.soundsManager.SpecialSoundsList(0, currentSanity);
            }
        }
    }

    private void HandleSlopes()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1f, ground);
        if (hit)
        {
            Debug.DrawRay(hit.point, Vector2.Perpendicular(hit.normal), Color.green);
            groundAngleVector = -Vector2.Perpendicular(hit.normal);
            float xMovement = Input.GetAxisRaw("Horizontal");
            if (Mathf.Abs(xMovement) < Mathf.Epsilon)
            {
                playerCollider.sharedMaterial = Vector2.Angle(hit.normal, Vector2.up) > 10f ? fullFrictionMaterial2D : slipperyMaterial2D;
            }
            else playerCollider.sharedMaterial = slipperyMaterial2D;
        }
        else groundAngleVector = Vector2.right;
    }

    private void HandleInput()
    {
        float xMovement = Input.GetAxisRaw("Horizontal");
        inputMovement = xMovement * groundAngleVector * moveSpeed;
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            playerRB.AddForce(Vector2.up * jumpForce);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            ToggleGhost();
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

    public float GetSanityLevel()
    {
        return currentSanity;
    }

    void ToggleGhost()
    {
        isGhost = !isGhost;
        playerSprite.color  = new Color(1,1,1, isGhost ? 0.5f : 1f);
        Physics2D.IgnoreLayerCollision(9,11, !isGhost);
        Physics2D.IgnoreLayerCollision(10,11, isGhost);
    }
}
