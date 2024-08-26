using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    private float moveInput;

    private Rigidbody2D rb;
    private Animator anim;

    private bool facingRight = true;

    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    private int extraJumps;
    public int extraJumpValue = 1;

    bool isTouchingFront;
    public Transform frontCheck;
    bool wallSliding;
    public float wallSlidingSpeed;

    bool wallJumping;
    public float xWallForce;
    public float yWallForce;
    public float wallJumpTime;

    public bool fallThrough;

    private enum State { idle, running, takeof, jumping, falling}
    
    private void Start()
    {
        extraJumps = extraJumpValue;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);


        moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        

        if (facingRight == false && moveInput > 0)
        {
            Flip();
        }
        else if (facingRight == true && moveInput < 0)
        {
            Flip();
        }
    }

    private void Update()
    {
        if(isGrounded == true)
        {
            extraJumps = extraJumpValue;
        }

        if(Input.GetKeyDown(KeyCode.Space) && extraJumps > 0)
        {
            rb.velocity = Vector2.up * jumpForce;
            extraJumps--; 
        }
        else if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true)
        {
            rb.velocity = Vector2.up * jumpForce;
        }


        isTouchingFront = Physics2D.OverlapCircle(frontCheck.position, checkRadius, whatIsGround);
        if( isTouchingFront == true && isGrounded == false && moveInput !=0)
        {
            wallSliding = true;
        }
        else
        {
            wallSliding = false;
        }   

        if (wallSliding)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }
        if (Input.GetKeyDown(KeyCode.Space) && wallSliding == true)
        {
            wallJumping = true;
            Invoke("SetWallJumpingToFalse", wallJumpTime);
        }
        if (wallJumping == true)
        {
            rb.velocity = new Vector2(xWallForce * -moveInput, yWallForce);
        }

        //OneWayPlatform
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            fallThrough = true;
        }
        else
        {
            fallThrough = false;
        }

        UpdateAnimationState();
        
    }

    private void Flip()
    {
        facingRight = !facingRight;

        transform.Rotate (0f, 180f, 0f);

    }

    private void SetWallJumpingToFalse()
    {
        wallJumping = false;
    }

    private void UpdateAnimationState()
    {
        State state;

        if (moveInput > 0f)
        {
            state = State.running;
        }
        else if (moveInput < 0f)
        {
            state = State.running;
        }
        else
        {
            state = State.idle;
        } 

        if (rb.velocity.y > .1f)
        {
             state= State.jumping;
        }
        else if (rb.velocity.y < -.1f)
        {
            state = State.falling;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            state = State.takeof;
        }

        anim.SetInteger("state", (int)state);
    }

}
