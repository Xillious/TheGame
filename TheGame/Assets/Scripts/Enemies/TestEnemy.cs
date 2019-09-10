using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : Enemy
{

    Vector3 positionTest;

    private float test;
    private float timerTest = 10f;

    float idleMin;
    float idleMax =5;
    float enemyIdleTime;

    public enum EnemyAIState
    {
        Idle,
        Wander,
        Aggro,
        Combat,
        Dead,
        Knockback
    }

    private bool wandering = false;
    private Vector3 wanderDestiaion;

    public EnemyAIState enemyState;

   
    IEnumerator Start()
    {
        //ChangeState(EnemyAIState.Idle);
        ChooseState();

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

                case EnemyAIState.Dead:
                    //enemy dead

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

        StateIndicator(idleColour);
        
    }

    private void Aggro()
    {
        
        if (isGrounded)
        rb.MovePosition(transform.position + transform.right * moveSpeed * Time.deltaTime);
        
        KnockbackCheck();

        //if the player leaves the chase radius choose a new state.
        if (!PlayerRangeCheck(chaseRadius))
        {
            ChooseState();
        }

        //if the player is within the attack radius change to the combat state.
        if(PlayerRangeCheck(attackRadius))
        {
            enemyState = EnemyAIState.Combat;
            
        }

        StateIndicator(aggroColor);
    }

    private void Combat()
    {
        KnockbackCheck();
        
        if (!PlayerRangeCheck(attackRadius))
        {
            ChooseState();
        }
    }

    private void Knockback()
    {
        //if the enemy is stationary
        if (rb.velocity.x == 0)
        {
            //change the 0 to recover quicker i think?.
            ChooseState();
        }
        StateIndicator(knockbackColour);
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
                wandering = false;
                StartCoroutine(CRT_Pause());
            }
        }

        if (Vector2.Distance(transform.position, target.transform.position) < chaseRadius)
        {
            ChangeState(EnemyAIState.Aggro);
            KnockbackCheck();
        }

        StateIndicator(wanderingColor);
    }

    //pause when the enemy reaches the wander destination.
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

            //Debug.Log("wandering to " + wanderDestiaion.ToString());
        }
    }


    private void FixedUpdate()
    {
        CheckSurroundings();
    }


    private void Update()
    {
        // if the wander position is outside the scene it hits the wall and flips.
        // if it hits the wall at all it starts wandering(fixes when it runs off after the player sort of)
        if (isTouchingWall || atEdgeOfPlatform)
        {
            ChangeState(EnemyAIState.Wander);
        }

        //Debug.Log(PlayerRangeCheck(healthBarRadius));
        //Debug.Log(enemyState);

        if (timerTest >= 0)
        {
            timerTest -= Time.deltaTime;
        } else if (timerTest <= 0)
        {
           // Debug.Log("ADFASFDGASFASFDASFAFASDF");
        }

        //Debug.Log(timerTest);


        




        //Debug.Log(CountdownTimer(5f));

        if (PlayerRangeCheck(6f))
        {
           
           // Debug.Log("Player in Range"); 
        }

        //target.transform.position = thePlayer.transform.position;

        //flips if the player is behind it
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

    /*
    // is the player in the range?
    private bool PlayerRangeCheck(float distance)
    {
        if (Vector3.Distance(transform.position, target.transform.position) < distance)
            return true;
        else
            return false;
    }
    */

    private void KnockbackCheck()
    {
       if (rb.velocity.x != 0)
        {
            ChangeState(EnemyAIState.Knockback);
        }
    }

    private void EnemyWander(float min, float max)
    {
        new Vector3(transform.position.x, transform.position.y, 0);
    }

    private void ChooseState()
    {
        test = Random.Range(0, 100);

        if (test > 50)
        {
            ChangeState(EnemyAIState.Idle);
        } else
        {
            ChangeState(EnemyAIState.Wander);
        }


    }


    private void IdleTime(float idleTime)
    {
        //Debug.Log(idleTime);
        if (idleTime >= 0)
        {
            idleTime -= Time.deltaTime;
        }
        else if (idleTime <= 0)
        {
            Debug.Log("ADFASFDGASFASFDASFAFASDF");

        }
    }

    private float CountdownTimer(float duration)
    {
        
         duration -= Time.deltaTime;
        return duration;
    }

    public void StateIndicator(Color stateColour)
    {
        stateIndicator.square.color = stateColour;   
    }

    private void OnDrawGizmos()
    {

        //Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y, wallCheck.position.z));
        //Gizmos.DrawLine(transform.position, wallCheckDistance);
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y + wallCheckDistance, transform.position.z));
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }

}



