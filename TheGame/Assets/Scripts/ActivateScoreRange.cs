using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateScoreRange : MonoBehaviour
{

    public GameObject item;

    public int scoreMin;
    public int scoreMax;

    private Score score;


    void Start()
    {
        score = FindObjectOfType<Score>();
    }

   
    void Update()
    {
        ShowItem(scoreMin, scoreMax);
    }

    void ShowItem(int min, int max)
    {
        if(score.score >= min && score.score <= max)
        {
            item.SetActive(true);
        }
        else if (score.score > max)
        {
            item.SetActive(false);
        }
    }
}
