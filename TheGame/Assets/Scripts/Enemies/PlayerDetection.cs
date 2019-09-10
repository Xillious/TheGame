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

        if (other.gameObject.layer == 9)
        {
            Debug.Log("Player in range");
            //enemy.target.position = other.transform.position;
           //enemy.thePlayer = other.gameObject;
        }
    }

   
}
