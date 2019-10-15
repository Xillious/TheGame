using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{

    public int score;
    Text scoreValue;
    
    void Start()
    {
        scoreValue = GetComponent<Text>();
    }

    void Update()
    {
        scoreValue.text = "Score: " + score;

        
    }

    public void AwardScore(int scoreIncrease)
    {
        score = score + scoreIncrease;
    }
}
