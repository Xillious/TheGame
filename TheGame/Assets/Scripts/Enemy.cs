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

    public string enemyName;

   

    private void Start()
    {
       
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player"))
        {
            Rigidbody2D player = other.collider.GetComponent<Rigidbody2D>();
            if (player != null)
            {
                Debug.Log("ENEMY HIT PLAYER");
                Vector2 difference = player.transform.position - transform.position;
                difference = difference.normalized * enemyKnockback;
                player.AddForce(difference, ForceMode2D.Impulse);
                StartCoroutine(CRT_EnemyKnockback(player));
            }
        }
    }

    private IEnumerator CRT_EnemyKnockback(Rigidbody2D player)
    {
        if (player != null)
        {
            yield return new WaitForSeconds(enemyKnockbackTime);
            player.velocity = Vector2.zero;

        }
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