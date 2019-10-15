using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateScoreExact : MonoBehaviour
{

    public GameObject item;

    public int requiredScore;

    private Score score;

    void Start()
    {
        score = FindObjectOfType<Score>();
    }

    // Update is called once per frame
    void Update()
    {
        ShowItem(requiredScore);
    }

    void ShowItem(int reqScore)
    {
        if (score.score == reqScore)
        {
            item.SetActive(true);
        }
    }
}
