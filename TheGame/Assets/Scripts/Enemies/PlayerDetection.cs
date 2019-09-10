using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    private TestEnemy enemy;
    


    private void Start()
    {
        GetComponentInParent<TestEnemy>();
    }

    private void Update()
    {
        //enemy.target = transform.Find("Player");

        if (Input.GetButtonUp("Jump"))
        {
            
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {

       // enemy.thePlayer = other.gameObject;

        // is the other the player?

        if (other.CompareTag("Player"))
        {
            Debug.Log("Player in range");

            // make the enemy target transform = player transofrm

            enemy.target = other.transform;
        }
    }

   
}
