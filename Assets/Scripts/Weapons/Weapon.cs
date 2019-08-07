using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float damage;
    public float attackTime;

    private PlayerController thePlayer;
    
    
    

    // Start is called before the first frame update
    void Start()
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
