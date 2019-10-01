using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public Transform newPosition;

    private GameMaster gameMaster;

    void Start()
    {
        gameMaster = FindObjectOfType<GameMaster>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            gameMaster.player.transform.position = newPosition.transform.position;
            //play sound?
        }
    }  
}
