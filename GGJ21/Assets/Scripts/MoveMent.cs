using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMent : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    private Rigidbody2D rb2D;
    private Vector2 moveDirection;
    private bool isJumping;

    private void Start()
    {
        Physics2D.IgnoreLayerCollision(9, 8);
        isJumping = false;
        rb2D = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            Jump();
        }
    }

    private void Jump()
    {
        rb2D.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        moveSpeed /= 2;
    }

    void FixedUpdate()
    {
        float horizontalMove = Input.GetAxis("Horizontal") * moveSpeed;
        moveDirection = new Vector2(horizontalMove, 0);
        Vector3 v = transform.TransformDirection(moveDirection) * moveSpeed;
        //rb2D.MovePosition(rb2D.position + new Vector2(v.x,v.y));
        rb2D.AddForce(new Vector2(v.x, v.y));
    }

    private void OnCollisionEnter2D(Collision2D player)
    {
        if (player.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
    }
    private void OnCollisionStay2D(Collision2D player)
    {
        if (player.gameObject.CompareTag("Ground"))
        {
            moveSpeed = 3;
        }
    }
    private void OnCollisionExit2D(Collision2D player)
    {
        if (player.gameObject.CompareTag("Ground"))
        {
            isJumping = true;
        }
    }
}
