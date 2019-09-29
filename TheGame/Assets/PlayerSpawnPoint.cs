using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnPoint : MonoBehaviour
{

    public Transform playerSpawnPoint;

    private GameMaster gameMaster;

    private void Awake()
    {
        gameMaster = FindObjectOfType<GameMaster>();
    }

    void Start()
    {
        if (gameMaster.player.transform.position != playerSpawnPoint.transform.position)
        {
            gameMaster.player.transform.position = playerSpawnPoint.transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
