using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : Enemy
{
    public enum State
    {
        Idle,
        Aggro,
        Combat
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
                    break;
            }
            yield return new WaitForEndOfFrame();
        }
    }

    private void Idle()
    {
        //Debug.Log("enemy is Idle");
        //if the trget (player) is in the enemy aggro range;
        if (Vector2.Distance(transform.position, target.transform.position) < aggroRange)
        {
            enemyState = State.Aggro;
        }
    }

    private void Aggro()
    {
        // transform.position = Vector2.MoveTowards(transform.position, target.transform.position, moveSpeed);
        //rb.velocity = new Vector2(moveSpeed * 1, rb.velocity.y);
        rb.MovePosition(transform.position + transform.right * moveSpeed * Time.deltaTime);
        //rb.MovePosition(transform.position + transform.position * moveSpeed * Time.deltaTime);
        //rb.MovePosition(transform.position + transform.localScale + new Vector3 (0, 0, 0) * moveSpeed * Time.deltaTime);
     

        if (Vector2.Distance(transform.position, target.transform.position) > aggroRange)
        {
            enemyState = State.Idle;
        }
    }

    private void Combat()
    {

    }




    private void Update()
    {

        CheckMovementDirection();

        //Debug.Log(rb.transform.right);

        
        if (Input.GetButtonDown("Attack"))
        {
            Flip();
            Debug.Log("Flip Enemy");
        }
        /*
        
        if (Vector2.Distance(transform.position, target.transform.position) < 2)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, moveSpeed);
            enemyState = State.Aggro;
        } else
        {
            enemyState = State.Idle;
        }
        */

        // Debug.Log(enemyState);
    }


}
