using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public string enemyName;

    public float moveSpeed;
    public float health = 5;
    public float maxHealth = 100;
    public float damage;
    public float enemyKnockback;
    public float enemyKnockbackTime;
    public float attackRadius;
    public float chaseRadius;
    public float healthBarRadius;
    public float attackCharge;
    public float attackCooldown;
    public float wallCheckDistance;
    public float wanderRangeMin;
    public float wanderRangeMax;
    public float groundCheckRadius;
    public float pauseTime;
    public float timeSpentIdle;
    public float maxIdleTime;
    public float playerImmunityTime = 1f;

    private int facingDirection = 1;

    public bool isGrounded;
    public bool isFacingRight;
    public bool isTouchingWall;
    public bool atEdgeOfPlatform;
    public bool backToPlayer;

    public bool targetInRange;

    
    public Transform wallCheck;
    public Transform groundCheck;

    [HideInInspector]
    public GameObject thePlayer;

    public Rigidbody2D rb;

    //public GameObject player;

    public LayerMask whatIsGround;
    public LayerMask platformEdge;
    public LayerMask playerLayer;

    public Color idleColour;
    public Color wanderingColor;
    public Color aggroColor;
    public Color knockbackColour;

    public EnemyStateIndicator stateIndicator;

    public HealthBar playerHealth;

    public Transform target;

    private void Awake()
    {
        stateIndicator = GetComponentInChildren<EnemyStateIndicator>();
    }

    private void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        playerHealth = FindObjectOfType<HealthBar>();
        
    }

    private void Update()
    {
        target.transform.position = thePlayer.transform.position;
        
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player"))
        {
            //knockback
            Rigidbody2D player = other.collider.GetComponent<Rigidbody2D>();
            if (player != null)
            {

                //knocks back the player
                other.collider.GetComponent<PlayerController>().StartKnockback(enemyKnockbackTime);

                //makes the palyer immune for a short period
                //other.collider.GetComponent<PlayerController>().PlayerIsImmune(playerImmunityTime);

                // Vector2 difference = player.transform.position - transform.position;
                // difference = difference.normalized * enemyKnockback;
                //player.AddForce(difference, ForceMode2D.Impulse);

                if (!other.collider.GetComponent<PlayerController>().isFacingRight)
                {
                    player.AddForce(new Vector3(1, 1, 0) * enemyKnockback, ForceMode2D.Impulse);
                }
                else if (other.collider.GetComponent<PlayerController>().isFacingRight)
                {
                    player.AddForce(new Vector3(-1, 1, 0) * enemyKnockback, ForceMode2D.Impulse);
                }

            }
           
        }
    }

    public void GetTarget()
    {
        if(target == null)
        {

        }
    }

    public void CheckMovementDirection()
    {
        if (isFacingRight && facingDirection > 0)
        {
            Flip();
        }
        else if (!isFacingRight && facingDirection < 0)
        {
            Flip();
        }
    }

    public void CheckSurroundings()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        //isTouchingWall = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, whatIsGround);
        isTouchingWall = Physics2D.Raycast(transform.position, transform.right, wallCheckDistance, whatIsGround);
        atEdgeOfPlatform = Physics2D.Raycast(transform.position, transform.right, wallCheckDistance, platformEdge);
        backToPlayer = Physics2D.Raycast(transform.position, transform.right * 180, chaseRadius, playerLayer);
    }

    public void Flip()
    {
        //facingDirection *= -1;
        isFacingRight = !isFacingRight;
        transform.Rotate(0, 180, 0);
    }

    public void TakeDamage(float damage)
    {
        //take damage
        health -= damage;
        //check if dead
        if (health <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        Destroy(gameObject);
    }

    // is the player in the range?
    public bool PlayerRangeCheck(float distance)
    {
        if (Vector3.Distance(transform.position, target.transform.position) < distance)
            return true;
        else
            return false;
    }

   

}





