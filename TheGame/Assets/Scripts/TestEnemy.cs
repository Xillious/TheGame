using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : Enemy
{

    Vector3 positionTest;

    private float test;

    public enum State
    {
        Idle,
        Aggro,
        Combat,
        Knockback
    }

    private State enemyState;

    IEnumerator Start()
    {
        enemyState = State.Idle;

        while (true)
        {
            switch (enemyState)
            {
                case State.Idle:
                    //run idle function
                    Idle();
                    break;
                case State.Aggro:
                    //run aggro function
                    Aggro();
                   
                    break;
                case State.Combat:
                    //run combat function
                    Combat();
                    break;
                case State.Knockback:
                    Knockback();
                    break;
            }
            yield return new WaitForEndOfFrame();
        }
    }

    private void Idle()
    {
        //Debug.Log("enemy is Idle");
        //if the trget (player) is in the enemy aggro range;
        if (Vector2.Distance(transform.position, target.transform.position) < chaseRadius)
        {
            enemyState = State.Aggro;
            KnockbackCheck();
        }
    }

    private void Aggro()
    {
        // transform.position = Vector2.MoveTowards(transform.position, target.transform.position, moveSpeed);
        //rb.velocity = new Vector2(moveSpeed * 1, rb.velocity.y);
        rb.MovePosition(transform.position + transform.right * moveSpeed * Time.deltaTime);
        //rb.MovePosition(transform.position + transform.position * moveSpeed * Time.deltaTime);
        //rb.MovePosition(transform.position + transform.localScale + new Vector3 (0, 0, 0) * moveSpeed * Time.deltaTime);

        KnockbackCheck();

        if (PlayerRangeCheck(chaseRadius))
        {
            enemyState = State.Idle;
        }

        if(PlayerRangeCheck(attackRadius))
        {
            enemyState = State.Combat;
        }

    }

    private void Combat()
    {

        KnockbackCheck();
        //GetComponentInChildren<EnemyAttack>().SwingWeapon();
        //other.collider.GetComponent<PlayerController>().StartKnockback(enemyKnockbackTime);
        if (!PlayerRangeCheck(attackRadius))
        {
            enemyState = State.Idle;
        }
    }

    private void Knockback()
    {
        //if the enemy is stationary
        if (rb.velocity.x == 0)
        {
            //change the 0 to recover quicker i think?.
            enemyState = State.Idle;
        }
    }

    private void FixedUpdate()
    {
        //Debug.Log(enemyState);
        //Debug.Log(rb.velocity.x);
    }

    private void Update()
    {

        

        if (isTouchingWall)
        {
            Debug.Log("Near Wall");
            Flip();
        }
        else if (!isTouchingWall)
        {

        }

        float distance = Vector3.Distance(transform.position, target.position);

        //Debug.Log(enemyState);

        if (PlayerRangeCheck(chaseRadius))
        {

            if ((transform.position.x > target.transform.position.x) && !isFacingRight)
            {
                
                Flip();
            }
            else if ((transform.position.x < target.transform.position.x) && isFacingRight)
            {
               
                Flip();
            }
        }

    }

    // is the player in the range?
    private bool PlayerRangeCheck(float distance)
    {
        if (Vector2.Distance(transform.position, target.transform.position) < distance)
            return true;
        else
            return false;
    }

    private void KnockbackCheck()
    {
       if (rb.velocity.x != 0)
        {
            enemyState = State.Knockback;
        }
    }

    private void OnDrawGizmos()
    {

        //Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y, wallCheck.position.z));
        //Gizmos.DrawLine(transform.position, wallCheckDistance);
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y + wallCheckDistance, transform.position.z));

    }

}



