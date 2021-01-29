using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;

    [Header("Speed Parameters")]
    [SerializeField] private float moveSpeed = 2.5f;
    [SerializeField] private float maxSpeed = 5f;
    [SerializeField] private float jumpForce = 8f;
    [SerializeField] private float lowJumpMultiplier = 2f;
    [SerializeField] private float fallMultiplier = 2.5f;
    [SerializeField] private float climbSpeed = 10f;
    [SerializeField] private float maxClimbSpeed = 7f;
    [Space]
    [Header("Ground Checking")]
    [SerializeField] private LayerMask ground;
    [SerializeField] private float groundCheckRadius = 0.5f;
    [SerializeField] private Transform groundCheckPositionTransform;
    [Space]
    [Header("Sanity")]
    [SerializeField] private float initialSanity = 100f;
    [SerializeField] private float sanityDegrationSpeed = 5f;
    [Space]
    [Header("Physics Materials")]
    [SerializeField] private PhysicsMaterial2D slipperyMaterial2D;
    [SerializeField] private PhysicsMaterial2D fullFrictionMaterial2D;
    [Space]
    [Header("Climb Checking")]
    [SerializeField] private LayerMask climbLayer;
    [SerializeField] private float climbCheckRadius = 0.5f;

    private Rigidbody2D playerRB;
    private Vector2 inputMovement;
    private Vector2 groundAngleVector;
    private Collider2D playerCollider;
    public float currentSanity;
    bool isSanity;
    private SpriteRenderer playerSprite;
    private bool inSafeZone;
    private State playerState;

    delegate void PlayerAction();
    private PlayerAction DoActionByState;

    delegate void PlayerInput();
    private PlayerInput InputByState;

    delegate void GroundCheck();
    GroundCheck CheckForGround;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        currentSanity = initialSanity;
        playerRB = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>();
        playerSprite = GetComponentInChildren<SpriteRenderer>();
        playerState = new Idle();
        DoActionByState = Move;
        InputByState = HandleInputNormal;
    }

    // Update is called once per frame
    void Update()
    {
        InputByState();
        HandleSlopes();
        CheckForGround?.Invoke();

        if (Input.GetKeyDown(KeyCode.H))
        {
            currentSanity -= 9;
            Debug.Log(currentSanity);
            if (currentSanity <= 40&& !isSanity)
            {
                isSanity = true;
                BlackBoard.soundsManager.TimeOutWhispers(0, currentSanity);
            }
        }

        //Debug.Log(CanClimb());
        //Debug.Log(playerState);
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

    private void HandleInputNormal()
    {
        float xMovement = Input.GetAxisRaw("Horizontal");
        playerState.HandleStateTransition(this, Mathf.Abs(xMovement) > 0 ? StateTransition.MovementDown : StateTransition.MovementUp);
        inputMovement = xMovement * groundAngleVector * moveSpeed;
        if (Input.GetButtonDown("Jump"))
        {
            playerState.HandleStateTransition(this, StateTransition.Jump);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            ToggleGhost();
        }

        if (Input.GetAxisRaw("Vertical") > 0)
        {
            playerState.HandleStateTransition(this, StateTransition.Climb);
        }
    }

    private void HandleInputClimbing()
    {
        inputMovement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * climbSpeed;
    }

    public void Jump()
    {
        playerRB.AddForce(Vector2.up * jumpForce);
        StartCoroutine(WaitToCheckForGround());
    }

    private IEnumerator WaitToCheckForGround()
    {
        yield return new WaitForSeconds(0.3f);
        CheckForGround = DoCheckForLanding;
    }

    void DoCheckForLanding()
    {
        if (IsGrounded())
        {
            playerState.HandleStateTransition(this, StateTransition.Land);
            BlackBoard.soundsManager.SoundsList(7);//jump sound
            CheckForGround = null;
        }
    }

    void FixedUpdate()
    {
        DoActionByState();
    }

    private void HandleJumpGravity()
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
        HandleJumpGravity();
    }

    public bool IsGrounded()
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
        if (!BlackBoard.gameManager.isGhostAbilityPicked) return;
        BlackBoard.gameManager.isGhost = !BlackBoard.gameManager.isGhost;
        playerSprite.color  = new Color(1,1,1, BlackBoard.gameManager.isGhost ? 0.5f : 1f);
        Physics2D.IgnoreLayerCollision(11,9, !BlackBoard.gameManager.isGhost);
        Physics2D.IgnoreLayerCollision(11,10, BlackBoard.gameManager.isGhost);
    }

    public void StartClimb()
    {
        playerRB.gravityScale = 0f;
        playerRB.velocity = new Vector2(playerRB.velocity.x, 0f);
        DoActionByState = DoClimb;
        InputByState = HandleInputClimbing;
    }

    void DoClimb()
    {
        playerRB.AddForce(inputMovement);
        Vector2 vel = playerRB.velocity;
        vel.x = Mathf.Clamp(vel.x, -maxClimbSpeed, maxClimbSpeed);
        vel.y = Mathf.Clamp(vel.y, -maxClimbSpeed, maxClimbSpeed);
        playerRB.velocity = vel;
        if (!CanClimb())
        {
            playerState.HandleStateTransition(this, StateTransition.MovementUp);
        }
    }

    public void SetNewState(State _s)
    {
        playerState = _s;
    }

    public bool CanClimb()
    {
        return Physics2D.OverlapCircle(transform.position, climbCheckRadius, climbLayer) != null;
    }

    public void StopClimbing()
    {
        playerRB.gravityScale = 1f;
        DoActionByState = Move;
        InputByState = HandleInputNormal;
    }

    public void SetInSafeZone(bool value)
    {
        inSafeZone = value;
    }
}
