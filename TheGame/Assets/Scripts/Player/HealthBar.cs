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

        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, MapHealth(health, maxHealth), lerpSpeed * Time.deltaTime);

        //clamps the player max health (should change to actual clamp)
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
        //can probably just lerp to new ammount.

    }

    private float MapHealth(float currentHealth, float maxHealth)
    {
        return currentHealth / maxHealth;
    }
}
