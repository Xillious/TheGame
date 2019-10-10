using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnPoint : MonoBehaviour
{

    public Transform playerSpawnPoint;

    //public GameMaster gameMaster;

    private Transform playerTransform;

    private void Awake()
    {
        //gameMaster = FindObjectOfType<GameMaster>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Start()
    {
       
            playerTransform.transform.position = playerSpawnPoint.transform.position;
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
