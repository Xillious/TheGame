using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private float lerpSpeed;

    Image healthBar;
    public float health;
    public float maxHealth;

    private PlayerController player;

    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        // find player health
        healthBar = GetComponent<Image>();

        //health = maxHealth;
        //health = player.playerMaxHealth;
    }

    void Update()
    {

        if (Input.GetButtonDown("Attack"))
        {
           // health -= 25f;
            
        }

        //healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, MapHealth(health, maxHealth), lerpSpeed * Time.deltaTime);
        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, MapHealth(player.playerHealth, player.playerMaxHealth), lerpSpeed * Time.deltaTime);

        //clamps the player max health (should change to actual clamp)
        if (player.playerHealth > player.playerMaxHealth)
        {
            player.playerHealth = player.playerMaxHealth;
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
