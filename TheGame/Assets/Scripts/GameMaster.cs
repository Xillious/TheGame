using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{

    private float timeLeft = 10;

    public GameObject player;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.R))
        {
            Instantiate(player, transform.position, transform.rotation);
        }

        timeLeft -= Time.deltaTime;
       // Debug.Log(timeLeft);

    }
}
