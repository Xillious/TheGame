using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public static bool atEndOfLevel = false;

    public GameObject pauseMenuUI;
    public GameObject endLevelUI;

    private Timer timer;

    GameMaster gameMaster;

    void Start()
    {
        timer = FindObjectOfType<Timer>();
        gameMaster = FindObjectOfType<GameMaster>();
    }

    // Update is called once per frame
    void Update()
    {

        if (timer.time == 0)
        {
            EndOfLevel();
        }


        if (Input.GetButtonDown("Start") && !atEndOfLevel)
        {

            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }


    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    void EndOfLevel()
    {
        endLevelUI.SetActive(true);
        Time.timeScale = 0f;
        atEndOfLevel = true;
        //gameMaster.gameOver = true;
    }
}
