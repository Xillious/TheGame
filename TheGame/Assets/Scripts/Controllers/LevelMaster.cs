using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMaster : MonoBehaviour
{
    public int levelTime = 60;

    private Timer timer;

    private Score score;

    public GameObject door;

    public int requiredScore;

    private void Awake()
    {
        timer = FindObjectOfType<Timer>();
    }
    void Start()
    {
        timer.SetStartTime(levelTime);
        Time.timeScale = 1f;
        score = FindObjectOfType<Score>();

    }

    // Update is called once per frame
    void Update()
    {
        UnlockNextLevel(requiredScore);
    }

    void UnlockNextLevel(int reqScore)
    {
        if (score.score >= reqScore)
        {
            if (door != null)
            {
                door.SetActive(true);
            }
        }
    }
}
