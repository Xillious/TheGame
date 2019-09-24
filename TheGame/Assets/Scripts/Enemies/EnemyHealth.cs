using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    private float lerpSpeed = 2f;

    [SerializeField]
    private Color fullColour;

    [SerializeField]
    private Color halfColour;

    [SerializeField]
    private Color lowColour;

    Vector3 localScale;

    private SpriteRenderer healthBar;

    private EnemyController enemy;

    

    void Start()
    {
        localScale = transform.localScale;
        enemy = GetComponentInParent<EnemyController>();
        healthBar = GetComponent<SpriteRenderer>();
    }

    
    void Update()
    {

        if (enemy.PlayerRangeCheck(enemy.healthBarRadius))
        {
            healthBar.enabled = true;
        } else if (!enemy.PlayerRangeCheck(enemy.healthBarRadius))
        {
            healthBar.enabled = false;
        }

        //lerps the colour of the health bar
        healthBar.color = Color.Lerp(lowColour, fullColour, localScale.x);

        //lerps between 3 colours based on if the enemy is above half
        //needs more work
        /*
        if (localScale.x > .5)
        {
            healthBar.color = Color.Lerp(halfColour, fullColour, localScale.x);
            Debug.Log("above");
        } else if (localScale.x < .5)
        {
            healthBar.color = Color.Lerp(lowColour, halfColour, localScale.x);
            Debug.Log("Below");
        }
        */



        localScale.x = Mathf.Lerp(localScale.x, MapHealth(enemy.health, enemy.maxHealth), lerpSpeed * Time.deltaTime);
        //localScale.x = MapHealth(enemy.health, enemy.maxHealth);
        transform.localScale = localScale;
    }

    
    private float MapHealth(float currentHealth,float maxHealth)
    {
        return currentHealth / maxHealth;
    }
    
   
}
