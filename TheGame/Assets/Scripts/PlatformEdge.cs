using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformEdge : MonoBehaviour
{

    private BoxCollider2D edgeCollider;
    private float time;
    private float timerLength;


    void Start()
    {
        edgeCollider = GetComponent<BoxCollider2D>();
        timerLength = Random.Range(5f, 10f);
    }

    
    void Update()
    {
        time -= Time.deltaTime;

        if (time <= 0)
        {
            time = timerLength;
            StartCoroutine(CRT_ActivateEdge());
        }
    }

    private IEnumerator CRT_ActivateEdge()
    {
        edgeCollider.enabled = false;
        yield return new WaitForSeconds(2);
        edgeCollider.enabled = true;
    }
}
