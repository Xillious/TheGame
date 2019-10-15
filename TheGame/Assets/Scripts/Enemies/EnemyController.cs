using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public enum EnemyAIState
    {
        Idle,
        Wander,
        Aggro,
        Combat,
        Dead,
        Knockback
    }

    public string enemyName;
    public string hitSound;

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
    float idleMin;
    float idleMax = 5;
    float enemyIdleTime;
    Vector3 positionTest;

    private float test;
    private float timerTest = 10f;

    public int enemyScoreValue;

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
    public GameObject blood;
    public GameObject enemyScoreText;

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

    private bool wandering = false;
    private Vector3 wanderDestiaion;

    private AudioManager audioManager;

    private KillCounter killcounter;

    private Score score;

    public EnemyAIState enemyState;

    private void Awake()
    {
        stateIndicator = GetComponentInChildren<EnemyStateIndicator>();
    }

    IEnumerator Start()
    {
        //target = GameObject.Find("Player").transform;
        target = FindObjectOfType<PlayerController>().transform;

        rb = GetComponent<Rigidbody2D>();
        playerHealth = FindObjectOfType<HealthBar>();
        score = FindObjectOfType<Score>();
        killcounter = FindObjectOfType<KillCounter>();

        audioManager = AudioManager.instance;
        if (audioManager == null)
        {
            Debug.LogError("No AudioManager found in scene.");
        }

        //ChangeState(EnemyAIState.Idle);
        //ChooseState();
        ChangeState(EnemyAIState.Wander);

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
        }
        else if (timerTest <= 0)
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

    private void FixedUpdate()
    {
        CheckSurroundings();
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
        if (PlayerRangeCheck(attackRadius))
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
        }
        else
        {
            ChangeState(EnemyAIState.Wander);
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

    public void StateIndicator(Color stateColour)
    {   
        if (stateIndicator != null)
        {
            stateIndicator.square.color = stateColour;
        }
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
        //Instantiate(blood, transform.position, transform.rotation);
        Instantiate(blood, transform.position, Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));
        //killcounter.EnemyKilled();
        //play death sound (blood hitting wall?)
        score.AwardScore(enemyScoreValue);
        if (enemyScoreText != null)
        {
            Instantiate(enemyScoreText, transform.position, transform.rotation = new Quaternion(0, 0, 0, 0));
        }
    }

    public bool PlayerRangeCheck(float distance)
    {
        if (Vector3.Distance(transform.position, target.transform.position) < distance)
            return true;
        else
            return false;
    }

    public void Flip()
    {
        //facingDirection *= -1;
        isFacingRight = !isFacingRight;
        transform.Rotate(0, 180, 0);
    }

    public void CheckSurroundings()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        //isTouchingWall = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, whatIsGround);
        isTouchingWall = Physics2D.Raycast(transform.position, transform.right, wallCheckDistance, whatIsGround);
        atEdgeOfPlatform = Physics2D.Raycast(transform.position, transform.right, wallCheckDistance, platformEdge);
        backToPlayer = Physics2D.Raycast(transform.position, transform.right * 180, chaseRadius, playerLayer);
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 12)
        {
            audioManager.PlaySound(hitSound);
        }
    }

    private void OnDrawGizmos()
    {

        //Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y, wallCheck.position.z));
        //Gizmos.DrawLine(transform.position, wallCheckDistance);
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y + wallCheckDistance, transform.position.z));
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
