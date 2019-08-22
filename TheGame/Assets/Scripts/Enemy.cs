using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed;
    public float attackCharge;
    public float damage;
    public float enemyKnockback;
    public float enemyKnockbackTime;
    public float atackRadius;
    public float chaseRadius;
    public float health = 5;
    public float attackCooldown;
    public float aggroRange;

    public int baseAttack;

    private int facingDirection = 1;

    public bool isGrounded;
    public bool isFacingRight;

    private bool targetInRange;

    public string enemyName;

    public PlayerController playerController;
    public Transform target;

    public Rigidbody2D rb;

    public LayerMask whatIsTarget;

    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        targetInRange = Physics2D.OverlapCircle(transform.position, 2f, whatIsTarget);

        

       
        Debug.Log(rb.velocity);

        /*
        //if player is in range move towards player.
        if (Vector2.Distance(transform.position, target.transform.position) < 2)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, moveSpeed);
        }
        */
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player"))
        {
            //knockback
            Rigidbody2D player = other.collider.GetComponent<Rigidbody2D>();
            if (player != null)
            {
                other.collider.GetComponent<PlayerController>().StartKnockback(enemyKnockbackTime);

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

    private void GetTarget()
    {
        if (target != null)
        {
            
        }     
    }

    public void CheckMovementDirection()
    {
        if (isFacingRight && facingDirection > 0)
        {
            Flip();
        } else if (!isFacingRight && facingDirection < 0)
        {
            Flip();
        }
    }

    public void Flip()
    {
        
            
            //facingDirection *= -1;
            isFacingRight = !isFacingRight;
            transform.Rotate(0, 180, 0);
           
    }


    private void OnCollisionExit2D(Collision2D other)
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
       
    }

    public void TakeDamage(float damage)
    {
        //hitpoints -= damage
        //check if dead.
        health -= damage;
        if (health <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        //ground check
       // Gizmos.DrawWireSphere(transform.position, 2f);

    }
}