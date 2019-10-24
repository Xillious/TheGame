using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToMenu : MonoBehaviour
{

    public GameObject canvas;
    private GameObject player;
    private GameObject gameMaster;


    public void Menu()
    {

        SceneManager.LoadScene("Menu");
        Destroy(canvas);
        Time.timeScale = 1f;

        player = GameObject.FindGameObjectWithTag("Player");
        gameMaster = GameObject.FindGameObjectWithTag("GM");
        Destroy(gameMaster);
        Destroy(player);
    }
}
