using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMaster : MonoBehaviour
{
    public int levelTime = 60;

    private Timer timer;

    private void Awake()
    {
        timer = FindObjectOfType<Timer>();
    }
    void Start()
    {
        timer.SetStartTime(levelTime);
        Time.timeScale = 1f;


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
