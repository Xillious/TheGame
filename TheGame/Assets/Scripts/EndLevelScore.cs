using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndLevelScore : MonoBehaviour
{
    //int score;
    Text scoreValue;

    Score playerScore;

    void Start()
    {
        scoreValue = GetComponent<Text>();
        playerScore = FindObjectOfType<Score>();
    }

    // Update is called once per frame
    void Update()
    {
         scoreValue.text = "" + playerScore.score;
       
    }
}
