﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using Random = UnityEngine.Random;

public class Player : MonoBehaviour
{
    public static Player instance;
    public AudioSource walkSound;
    public AudioClip[] stepsSounds;

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
    [Space] [SerializeField] private LayerMask ghostLayer;

    private Rigidbody2D playerRB;
    private Vector2 inputMovement;
    private Vector2 groundAngleVector;
    private Collider2D playerCollider;
    public float currentSanity;
    bool isSanity;
    public SanityBar sanityBar;
    private SpriteRenderer playerSprite;
    private bool inSafeZone;
    private State playerState;
    private bool facingRight;
    private Animator playerAnim;

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
        walkSound = GetComponent<AudioSource>();
        currentSanity = initialSanity;
        sanityBar.SetMaxSenity(currentSanity);
        playerRB = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<Animator>();
        playerCollider = GetComponent<Collider2D>();
        playerSprite = GetComponentInChildren<SpriteRenderer>();
        playerState = new Idle();
        DoActionByState = Move;
        InputByState = HandleInputNormal;
        BlackBoard.gameManager.ToggleGhost += value => playerAnim.SetBool("Is Ghost", value);
    }

    void Update()
    {
        InputByState();
        CheckForGround?.Invoke();
        HandleSanity();
    }

    private void HandleSanity()
    {
        if (!inSafeZone)
        {
            currentSanity -= Time.deltaTime;
            sanityBar.SetSenity(currentSanity);
            if (currentSanity <= 40 && !isSanity)
            {
                isSanity = true;
                BlackBoard.soundsManager.TimeOutWhispers(0, currentSanity);
            }
        }
    }

    private void HandleInputNormal()
    {
        float xMovement = Input.GetAxisRaw("Horizontal");
        playerState.HandleStateTransition(this, Mathf.Abs(xMovement) > 0 ? StateTransition.MovementDown : StateTransition.MovementUp);
        playerAnim.SetFloat("Move Speed", Mathf.Abs(xMovement));
        inputMovement = xMovement * Vector2.right * moveSpeed;
        CheckForFlip();
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
        CheckForFlip();
    }

    void CheckForFlip()
    {
        if (inputMovement.x > 0 && !facingRight || 
            inputMovement.x < 0 && facingRight)
            Flip();
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
            if(!BlackBoard.gameManager.GetGhostStatus())
            {
                BlackBoard.soundsManager.SoundsList(7);//jump sound
            }
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
        if (Mathf.Abs(inputMovement.x) > 0 && IsGrounded() && !BlackBoard.gameManager.GetGhostStatus())
        {
            if (!walkSound.isPlaying)
            {
                int randomSound = Random.Range(0, stepsSounds.Length);
                walkSound.PlayOneShot(stepsSounds[randomSound]);
            }
        }
        else
        {
            walkSound.Stop();
        }
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

    void ToggleGhost()
    {
        if (!BlackBoard.gameManager.GetGhostAbilityPicked() ||
            Physics2D.OverlapCircle(transform.position, climbCheckRadius, ghostLayer) != null) return;
        BlackBoard.gameManager.ToggleGhostStatus();
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
        if (!BlackBoard.soundsManager.voicesSFX.isPlaying)
        {
            BlackBoard.soundsManager.SoundsList(12);
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
        if (value)
        {
            currentSanity = initialSanity;
            sanityBar.SetSenity(currentSanity);
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        transform.rotation = Quaternion.Euler(Vector3.up * (facingRight ? 180f : 0f));
    }

}
