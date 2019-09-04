using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : Enemy
{

    Vector3 positionTest;

    private float test;

    public enum EnemyAIState
    {
        Idle,
        Wander,
        Aggro,
        Combat,
        Knockback
    }

    private bool wandering = false;
    private Vector3 wanderDestiaion;

    private EnemyAIState enemyState;

   
    IEnumerator Start()
    {
        ChangeState(EnemyAIState.Idle);

        while (true)
        {
            switch (enemyState)
            {
                case EnemyAIState.Idle:
                    //run idle function
                    Idle();
                   
                    break;

                case EnemyAIState.Wander:
                    if (isGrounded)
                        Wander();
                    break;

                case EnemyAIState.Aggro:
                    //run aggro function
                     Aggro();
                    break;

                case EnemyAIState.Combat:
                    //run combat function
                    Combat();
                    break;

                case EnemyAIState.Knockback:
                    Knockback();
                    break;
            }
            yield return new WaitForEndOfFrame();
        }
    }

    private void Idle()
    {
       
        //if the trget (player) is in the enemy aggro range;
        if (Vector2.Distance(transform.position, target.transform.position) < chaseRadius)
        {
            ChangeState(EnemyAIState.Aggro);
            KnockbackCheck();
        }
    }

    private void Aggro()
    {
        
        if (isGrounded)
        rb.MovePosition(transform.position + transform.right * moveSpeed * Time.deltaTime);
        
        KnockbackCheck();

        if (PlayerRangeCheck(chaseRadius))
        {
            enemyState = EnemyAIState.Idle;
        }

        if(PlayerRangeCheck(attackRadius))
        {
            enemyState = EnemyAIState.Combat;
        }

    }

    private void Combat()
    {

        KnockbackCheck();
        
        if (!PlayerRangeCheck(attackRadius))
        {
            ChangeState(EnemyAIState.Idle);
        }
    }

    private void Knockback()
    {
        //if the enemy is stationary
        if (rb.velocity.x == 0)
        {
            //change the 0 to recover quicker i think?.
            //enemyState = EnemyAIState.Idle;
            ChangeState(EnemyAIState.Idle);
        }
    }

    private void Wander()
    {
        if (wandering)
        {
            // move forward
            rb.MovePosition(transform.position + transform.right * moveSpeed * Time.deltaTime);

            // have we reached the wander destination?
            if (Vector3.Distance(transform.position, wanderDestiaion) < 0.1f)
            {
                // reached destination - wait a while then wander again
                Debug.Log("reached destination");
                wandering = false;
                StartCoroutine(CRT_Pause());
            }
        }
        
    }

    private IEnumerator CRT_Pause()
    {
        float pause = Random.Range(0, pauseTime);
        yield return new WaitForSecondsRealtime(pause);
        ChangeState(EnemyAIState.Wander);
    }

    public void ChangeState(EnemyAIState newState)
    {
        enemyState = newState;

        if (newState == EnemyAIState.Wander)
        {
            // pick a wander desination
            float dist = Random.Range(wanderRangeMin, wanderRangeMax);

            // pick a side
            if (Random.value > 0.5f)
            {
                // face left
                if (isFacingRight)
                    Flip();
                dist = -dist;
            }
            else
            {
                // going right
                if (!isFacingRight)
                    Flip();
            }
            wanderDestiaion = transform.position + Vector3.right * dist;
            wandering = true;

            Debug.Log("wandering to " + wanderDestiaion.ToString());
        }
    }


    private void FixedUpdate()
    {
        CheckSurroundings();
    }

    private void Update()
    {

        if (isTouchingWall)
        {
            ChangeState(EnemyAIState.Idle);
        }
        


        if (PlayerRangeCheck(chaseRadius))
        {

            if ((transform.position.x > target.transform.position.x) && isFacingRight)
            {
                
                Flip();
            }
            else if ((transform.position.x < target.transform.position.x) && !isFacingRight)
            {
               
                Flip();
            }
        }

    }

    // is the player in the range?
    private bool PlayerRangeCheck(float distance)
    {
        if (Vector3.Distance(transform.position, target.transform.position) < distance)
            return true;
        else
            return false;
    }

    private void KnockbackCheck()
    {
       if (rb.velocity.x != 0)
        {
            enemyState = EnemyAIState.Knockback;
        }
    }

    private void EnemyWander(float min, float max)
    {
        new Vector3(transform.position.x, transform.position.y, 0);
    }

    private void OnDrawGizmos()
    {

        //Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y, wallCheck.position.z));
        //Gizmos.DrawLine(transform.position, wallCheckDistance);
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y + wallCheckDistance, transform.position.z));
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }

}



