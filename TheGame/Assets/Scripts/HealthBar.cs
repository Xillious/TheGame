using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private float lerpSpeed;

    Image healthBar;
    public static float health;
    public float maxHealth;

    void Start()
    {
        // find player health
        healthBar = GetComponent<Image>();

        health = maxHealth;
        //should be health = playerMaxHealth


    }

   
    void Update()
    {

        if (Input.GetButtonDown("Attack"))
        {
            health -= 25f;
            
        }

        //healthBar.fillAmount = health / maxHealth;
        //healthBar.fillAmount = MapHealth(health, maxHealth);
        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, MapHealth(health, maxHealth), lerpSpeed * Time.deltaTime);


        if (health > maxHealth)
        {
            health = maxHealth;
        }

    }

    private void GainHealth(float amount)
    {
        //how much to gain
        //set current health
        //gain health 1 at a time until you have gained the set amount

    }

    private float MapHealth(float currentHealth, float maxHealth)
    {
        return currentHealth / maxHealth;
    }
}
