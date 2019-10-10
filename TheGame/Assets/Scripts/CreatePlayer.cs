using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatePlayer : MonoBehaviour
{

    private GameMaster gameMaster;
    float spawntime;
    bool playerSpawned;

    void Start()
    {
        gameMaster = FindObjectOfType<GameMaster>();
        //Instantiate(gameMaster.player, transform.position, transform.rotation);
    }

    
    void Update()
    {
        if (spawntime < 0 && !playerSpawned)
        {
            Instantiate(gameMaster.player, transform.position, transform.rotation);
            playerSpawned = true;
        }
        else
        {
            spawntime -= Time.deltaTime;
        }
       
    }
}
