using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{

    private float timeLeft = 10;

    public bool gameOver = false;

    public GameObject player;

    public Canvas canvas;

    public GameObject doorOne;
    public GameObject doorTwo;

    void Start()
    {
        canvas = FindObjectOfType<Canvas>();
        
    }

   
    void Update()
    {
      

        /*
        if (Input.GetKeyDown(KeyCode.G))
        {
            doorOne.GetComponent<Door>().OpenDoor();
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            doorTwo.GetComponent<Door>().OpenDoor();
        }
        */

        
        

        if (Input.GetKeyUp(KeyCode.R))
        {
            //Instantiate(player, transform.position, transform.rotation);
        }

        timeLeft -= Time.deltaTime;
       
    }

    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void FirstLevel()
    {
        SceneManager.LoadScene("Lobby");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
