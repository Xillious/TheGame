using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public int time;
    Text currentTime;
    public float timerSpeed = 1;

    void Start()
    {
        currentTime = GetComponent<Text>();
        StartCoroutine(LoseTime());
    }

    
    void Update()
    {
        currentTime.text = time.ToString();
    
        if (time <= 5)
        {
            timerSpeed = 1.5f;
        }

        if (time == 0)
        {
            StopAllCoroutines();
        }
    }

    IEnumerator LoseTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(timerSpeed);
            time--;
        }
    }

    public void SetStartTime(int startTime)
    {
        time = startTime;
    }
}
