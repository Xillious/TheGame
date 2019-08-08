using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float movementInputDirection;
    private float moveSpeed;                                 // player movement speed
    private float jumpForce;                                 // how hight the player jumps
    private float groundCheckRadius;                         // the radius of the circle that cheks for ground
    private float wallCheckDistance;                         // raycast that checks how close to the wall the player is
    private float wallSlidingSpeed;                          // how slow the player slides down the wall
    private float movementforceInAir;                        // how fast/slow the player moves in the air    
    private float airDragMultiplier;                         // the air resistance on the player
    private float variableJumpHeightMultiplier;              // varies the jump height (1 is full height)
    private float wallHopForce;                              // how high the player jumps up the wall
    private float wallJumpForce;                             // how far the player jumps away from the wall
    private float leftWallTime;

    private int amountOfJumpsLeft;
    private int facingDirection = 1;
    private int amountOfJumps;                              // amount of jumps -_-

    private bool isFacingRight = true;
    private bool isGrounded;                                // is the player currnetly on the ground
    private bool isTouchingWall;                            // is the player touching the wall
    private bool isWallSliding;                             // is the player currently wall sliding
    private bool canJump;                                   // can the player jump
    private bool isRunning;
    private bool isSliding;
    private bool isCrouching;
    private bool isAttacking;
    

    private Rigidbody2D rb;
    private Animator anim;

    public bool hasWeapon = false;

    public int int_amountOfJumps = 1;                           // amount of jumps -_-

    public float int_moveSpeed;
    public float int_jumpForce;
    public float int_groundCheckRadius;
    public float int_wallCheckDitance;
    public float int_wallSlidingSpeed;
    public float int_movementforceInAir;
    public float int_airDragMultiplier = 0.8f;                  
    public float int_variableJumpHeightMultiplier = 0.5f;       
    public float int_wallHopForce;                              
    public float int_wallJumpForce;                             
    public float int_leftWallTime = 0.2f;

    public Vector2 wallHopDirection;
    public Vector2 wallJumpDirection;

    public Transform groundCheck;
    public Transform wallCheck;

    public LayerMask whatIsGround;

    public GameObject myWeapon;

    //public scr_hitbox hitboxScript;
    //
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        amountOfJumpsLeft = amountOfJumps;
        wallHopDirection.Normalize();
        wallJumpDirection.Normalize();
        InitialiseVariables();
    }

    
    void Update()
    {
        CheckInput();
        CheckMovementDirection();
        UpdateAnimations();
        CheckIfCanJump();
        CheckIfWallSliding();
        //Debug.Log(rb.velocity.y);

        // do an attack
        // GetButtonDown only runs once
        if (Input.GetButtonDown("Attack"))
        {
            // if we have a weapon tell it to do its attack
            if (myWeapon != null)
            {
                myWeapon.SendMessage("Attack");
            }
        }
    }

    private void FixedUpdate()
    {
        ApplyMovement();
        CheckSurroundings();
    }

    private void UpdateAnimations()
    {
        anim.SetBool("isRunning", isRunning);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("yVelocity", rb.velocity.y);
        anim.SetBool("isWallSliding", isWallSliding);
        anim.SetBool("isCrouching", isCrouching);
        anim.SetFloat("xVelocity", rb.velocity.x);
    }

    private void CheckIfWallSliding()
    {
        if(isTouchingWall && !isGrounded && rb.velocity.y < 0 && movementInputDirection != 0) //MovementInputDirection != 0 makes you have to hold the wall
        {
            isWallSliding = true;
        }
        else
        {
            isWallSliding = false;
        }
    }

    private void CheckSurroundings()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        isTouchingWall = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, whatIsGround);
    }

    private void CheckIfCanJump()
    {
        if ((isGrounded && rb.velocity.y <= 0) || isWallSliding)
        {
            amountOfJumpsLeft = amountOfJumps;
        }

        if (amountOfJumpsLeft <= 0)
        {
            canJump = false;
        }
        else
        {
            canJump = true;
        }
    }

    private void CheckMovementDirection()
    {
        if (isFacingRight && movementInputDirection < 0)
        {
            Flip();
        } else if (!isFacingRight && movementInputDirection > 0)
        {
            Flip();
        }

        if (rb.velocity.x != 0)
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }
    }

    private void CheckInput()
    {
        movementInputDirection = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        if (Input.GetButtonUp("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * variableJumpHeightMultiplier);
        }

        if (Input.GetButton("Crouch") && isGrounded)
        {
            isCrouching = true;
        }
        else
        {
            isCrouching = false;
        }
    }

    private void Jump()
    {
        if(canJump && !isWallSliding && leftWallTime > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            amountOfJumpsLeft--;
        }
        
        else if (isWallSliding && movementInputDirection == 0 && canJump && leftWallTime > 0) // wall hop
        {
            isWallSliding = false;
            amountOfJumpsLeft--;
            Vector2 forceToAdd = new Vector2(wallHopForce * wallHopDirection.x * -facingDirection, wallHopForce * wallHopDirection.y);
            rb.AddForce(forceToAdd, ForceMode2D.Impulse);
            //Debug.Log("Hop");
        }
        else if ((isWallSliding || isTouchingWall) && movementInputDirection != 0 && canJump && leftWallTime > 0) // wall jump
        {
            isWallSliding = false;
            amountOfJumpsLeft--;
            Vector2 forceToAdd = new Vector2(wallJumpForce * wallJumpDirection.x * movementInputDirection, wallJumpForce * wallJumpDirection.y);
            rb.AddForce(forceToAdd, ForceMode2D.Impulse);
            //Debug.Log("Jump");
        }
        
    }

    private void ApplyMovement()
    {
        if (isGrounded)
        {
            rb.velocity = new Vector2(moveSpeed * movementInputDirection, rb.velocity.y);
            leftWallTime = int_leftWallTime; ;
        }
        else if (!isGrounded && !isWallSliding && movementInputDirection != 0)
        {
            Vector2 forceToAdd = new Vector2(movementforceInAir * movementInputDirection, 0);
            rb.AddForce(forceToAdd);

            if (Mathf.Abs(rb.velocity.x) > moveSpeed)
            {
                rb.velocity = new Vector2(moveSpeed * movementInputDirection, rb.velocity.y);
            }
        }
        else if (!isGrounded && !isWallSliding && movementInputDirection == 0)
        {
            rb.velocity = new Vector2(rb.velocity.x * airDragMultiplier, rb.velocity.y);
        }

        if (isWallSliding)
        {
            if(rb.velocity.y < -wallSlidingSpeed)
            {
                rb.velocity = new Vector2(rb.velocity.x, -wallSlidingSpeed);
                leftWallTime = int_leftWallTime;
            }
        }

        if (isCrouching)
        {
            moveSpeed = 5;
            //Debug.Log("Crouching");
        }
        else
        {
            moveSpeed = int_moveSpeed;
        }

        if (!isGrounded && !isWallSliding && movementInputDirection != 0)
        {
            leftWallTime -= Time.deltaTime;
        }
    }

    private void Flip()
    {
        if (!isWallSliding)
        {
            // if its inside here the player cant just let go of the wall
            facingDirection *= -1;
            isFacingRight = !isFacingRight;
            transform.Rotate(0, 180, 0);
           // GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
        }
        
    }
    private void InitialiseVariables()
    {
        moveSpeed = int_moveSpeed;
        jumpForce = int_jumpForce;
        groundCheckRadius = int_groundCheckRadius;
        wallCheckDistance = int_wallCheckDitance;
        wallSlidingSpeed = int_wallSlidingSpeed;
        movementforceInAir = int_movementforceInAir;
        airDragMultiplier = int_airDragMultiplier;
        variableJumpHeightMultiplier = int_variableJumpHeightMultiplier;
        wallHopForce = int_wallHopForce;
        wallJumpForce = int_wallJumpForce;
        leftWallTime = int_leftWallTime;
        amountOfJumps = int_amountOfJumps;
}

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);

        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y, wallCheck.position.z));
    }

   
}
