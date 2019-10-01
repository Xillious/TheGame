using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{

    private float timeLeft = 10;

    public GameObject player;

    public Canvas canvas;


    public GameObject doorOne;
    public GameObject doorTwo;

    void Start()
    {
        canvas = FindObjectOfType<Canvas>();
        //doorOne.GetComponent<Door>();
        //doorTwo.GetComponent<Door>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            doorOne.GetComponent<Door>().OpenDoor();
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            doorTwo.GetComponent<Door>().OpenDoor();
        }




        if (Input.GetKeyUp(KeyCode.R))
        {
            Instantiate(player, transform.position, transform.rotation);
        }

        timeLeft -= Time.deltaTime;
       // Debug.Log(timeLeft);

    }

    public void FirstLevel()
    {
        SceneManager.LoadScene("Lobby");
    }
}
