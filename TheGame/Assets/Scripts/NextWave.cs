using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextWave : MonoBehaviour
{
    private WaveSpawner waveSpawner;

    private void Awake()
    {
        waveSpawner = FindObjectOfType<WaveSpawner>();
    }

    void Start()
    {
        waveSpawner.WaveCompleted();
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject);
    }
}
