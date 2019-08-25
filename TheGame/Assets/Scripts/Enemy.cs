using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public string enemyName;

    public float moveSpeed;
    public float health = 5;
    public float damage;
    public float enemyKnockback;
    public float enemyKnockbackTime;
    public float atackRadius;
    public float chaseRadius;
    public float attackCharge;
    public float attackCooldown;
    public float wallCheckDistance;

    private int facingDirection = 1;

    public bool isGrounded;
    public bool isFacingRight;
    public bool isTouchingWall;
    public bool backToPlayer;

    private bool targetInRange;

    public Transform target;
    public Transform wallCheck;

    public Rigidbody2D rb;

    public GameObject player;

    public LayerMask whatIsGround;

    public LayerMask playerLayer;


    private void Start()
    {

        rb = GetComponent<Rigidbody2D>();
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
        //isTouchingWall = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, whatIsGround);
        isTouchingWall = Physics2D.Raycast(transform.position, transform.right, wallCheckDistance, whatIsGround);
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


}





