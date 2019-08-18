using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float damage;
    public float attackBuildup;
    public float attackDuration;
    public float knockback;
    public float knockbackTime;

    public PlayerController thePlayer;
    
    
    

    // Start is called before the first frame update
    void Awake()
    {
        thePlayer = FindObjectOfType<PlayerController>();
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // if the player has no weapon, pick this up
        if (thePlayer.myWeapon == null)
        {
            thePlayer.myWeapon = this.gameObject;
                // do a pickup sound + animation
        }

        
        
    }


    // Update is called once per frame
    void Update()
    {
    }

   
}
