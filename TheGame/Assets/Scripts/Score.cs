using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{

    public int score;
    Text scoreValue;

    public int kills;
    
    void Start()
    {
        scoreValue = GetComponent<Text>();
    }

    void Update()
    {
        scoreValue.text = "Score: " + score;

        //Debug.Log(kills);
        if (kills == 10)
        {
            //Debug.Log("10 Kills");
        }
    }

    public void AwardScore(int scoreIncrease)
    {
        score = score + scoreIncrease;
    }

    public void EnemyKilled()
    {
        kills = kills + 1;
    }
}
