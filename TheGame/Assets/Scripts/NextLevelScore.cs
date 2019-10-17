using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextLevelScore : MonoBehaviour
{

    public int requiredScore;

    Text scoreValue;

    private void Awake()
    {
        scoreValue = GetComponent<Text>();
    }

    void Start()
    {
       
    }

    
    void Update()
    {
        scoreValue.text = "next level " + requiredScore;
        Debug.Log(requiredScore);
    }
}
