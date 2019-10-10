using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreOnTreasure : MonoBehaviour
{
    public float startTime;
    private float time;
    public float speed = 1;

    void Start()
    {
        time = startTime;
        
    }


    void Update()
    {
        transform.Translate(Vector3.up *(speed * Time.deltaTime));
        time -= Time.deltaTime;

        if (time < 0)
        {
            Destroy(gameObject);
        }
    }
}
