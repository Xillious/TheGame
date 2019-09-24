using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTimer : MonoBehaviour
{

    public float startTime;
    private float time;

    void Start()
    {
        time = startTime;
    }

    
    void Update()
    {
        time -= Time.deltaTime;

        if (time < 0)
        {
            Destroy(gameObject);
        }
    }
}
