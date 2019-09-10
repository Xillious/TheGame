using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerImmunity : MonoBehaviour
{

    public BoxCollider2D hitbox;

    void Start()
    {
        hitbox = GetComponent<BoxCollider2D>();
    }

    
    void Update()
    {
        
    }
}
