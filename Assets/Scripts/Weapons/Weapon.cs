using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float damage;
    public float attackTime;

    private PlayerController thePlayer;
    public GameObject player;
    
    

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

        
        SetParent(player);
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetParent(GameObject newParent)
    {
        transform.SetParent(newParent.transform);
        transform.localPosition = Vector3.zero;
    }

    private void DetatchFromParent()
    {
        transform.parent = null;
    }
}
