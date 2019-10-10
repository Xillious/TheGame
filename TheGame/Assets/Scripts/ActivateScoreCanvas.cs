using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateScoreCanvas : MonoBehaviour
{

    public GameObject score;
    private Canvas canvas;

    void Start()
    {
        canvas = Canvas.FindObjectOfType<Canvas>();
        score = GameObject.FindGameObjectWithTag("Score");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //score.gameObject.SetActive(true);
            //canvas.enabled = true;
        }
    }
}
