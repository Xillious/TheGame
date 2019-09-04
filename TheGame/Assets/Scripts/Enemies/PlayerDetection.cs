using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    private Enemy enemy;


    private void Start()
    {
        GetComponentInParent<Enemy>();
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
       
        if (other.gameObject.layer == 9)
        {
            Debug.Log("Player in range");
            enemy.target = other.transform; 
        }
    }

   
}
