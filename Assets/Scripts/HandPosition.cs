using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandPosition : MonoBehaviour
{
    public PlayerController thePlayer;
    Vector3 defaultPosition;
    Vector3 crouchPosition;


    void Start()
    {
        thePlayer = FindObjectOfType<PlayerController>();
        crouchPosition = new Vector3(0, 5, 0);
        defaultPosition = new Vector3(0.31f, -0.19f, 0);
    }

    
    void Update()
    {
        if (thePlayer.isCrouching)
        {
            transform.position = crouchPosition;
        } else if (!thePlayer.isCrouching)
        {
            //transform.position = defaultPosition;
        }
    }
}
