using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayOnTrigger : MonoBehaviour
{

    private BoxCollider2D hitBox;
    public SpriteRenderer sprite;
    public GameObject gameObject;

    void Start()
    {
        //hitBox = GetComponentInChildren<BoxCollider2D>();
        //sprite = GetComponent<SpriteRenderer>();
        hitBox = GetComponent<BoxCollider2D>();
        sprite = gameObject.GetComponent<SpriteRenderer>();
    }

    
    void Update()
    {
        
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            sprite.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            sprite.enabled = false;
        }
    }
    /*
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player in range");
            sprite.enabled = true;
           
        } else
        {
            sprite.enabled = false;
        }
    }
    */
}
