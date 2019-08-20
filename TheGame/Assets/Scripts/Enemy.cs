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
    public int baseAttack;

    public bool isGrounded;
    public bool isFacingRight;

    public string enemyName;

    public PlayerController playerController;

    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player"))
        {
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

 

    private IEnumerator CRT_EnemyKnockback(Rigidbody2D player)
    {
        if (player != null)
        {
            yield return new WaitForSeconds(enemyKnockbackTime);
            player.velocity = Vector2.zero;
            //playerController.beingKnockedBack = false;
        }
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
}