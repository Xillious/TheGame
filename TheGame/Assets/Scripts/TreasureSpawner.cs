﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureSpawner : MonoBehaviour
{
    public enum TreasureSpawnState
    {
        Counting,
        Spawning,
        Waiting,
        Finished
    }

    [System.Serializable]
    public class Treasure
    {
        public string name;
        public Transform[] treasure;
        public int count;
        public float spawnRate;
    }

    public Treasure[] treasures;

    public Transform[] spawnPoints;


    public float countdown;
    public float countdownTime;
    public float waiting;
    public float waitingTime;

    private int nextWave = 0;
    private int spawnPointPosition = -1;

    private TreasureSpawnState treasureSpawnState;

    private IEnumerator Start()
    {

        countdown = countdownTime;
        waiting = waitingTime;

        ChangeState(TreasureSpawnState.Counting);

        while (true)
        {
            switch (treasureSpawnState)
            {
                case TreasureSpawnState.Counting:
                    //run counting
                    Counting();
                    break;

                case TreasureSpawnState.Spawning:
                    //run spawning
                    break;

                case TreasureSpawnState.Waiting:
                    //run waiting
                    Waiting();
                    break;

                case TreasureSpawnState.Finished:
                    //run finished
                    break;
            }
            yield return new WaitForEndOfFrame();
        }
    }

    void Update()
    {
        //Debug.Log(treasureSpawnState);
        UpdateSpawnPoints();
        //Debug.Log(spawnPointPosition);
    }

    void Counting()
    {
        if (countdown <= 0)
        {
            if (treasureSpawnState != TreasureSpawnState.Spawning)
            {
                StartCoroutine(CRT_SpawnTreasure(treasures[nextWave]));
            }
        }
        else if (countdown > 0)
        {
            countdown -= Time.deltaTime;
        }
    }

    void Spawning()
    {

    }

    void Waiting()
    {
        if (waiting <= 0)
        {
            WaveCompleted();
        }
        else if (waiting > 0)
        {
            waiting -= Time.deltaTime;
        } 
    }

    void Finished()
    {

    }

    void WaveCompleted()
    {
        //Debug.Log("waeCompleted");
        ChangeState(TreasureSpawnState.Counting);

        if (nextWave + 1 > treasures.Length - 1)
        {
            Debug.Log("Finished");
        }
        else
        {
            nextWave++;
        }
    }

    IEnumerator CRT_SpawnTreasure(Treasure treasure)
    {
        ChangeState(TreasureSpawnState.Spawning);

        for (int i = 0; i < treasure.count; i++)
        {
            SpawnTreasure(treasure.treasure[0]);
            yield return new WaitForSeconds(treasure.spawnRate);
        }

        ChangeState(TreasureSpawnState.Waiting);
        yield break;
    }

    void SpawnTreasure(Transform treasure)
    {
        //Debug.Log("Spawning Treasure" + treasure.name);
        spawnPointPosition = spawnPointPosition + 1;
        Transform sp = spawnPoints[spawnPointPosition];
        Instantiate(treasure, sp.position, sp.rotation);

      
    }

    void SpawnTreasureRandom(Transform treasure)
    {
        //Debug.Log("Spawning Treasure" + treasure.name);
        spawnPointPosition = spawnPointPosition + 1;
        Transform sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(treasure, sp.position, sp.rotation);
    }

    private void ChangeState(TreasureSpawnState newState)
    {
        treasureSpawnState = newState;
    }

    private void UpdateSpawnPoints()
    {
        if (spawnPointPosition >= spawnPoints.Length - 1)
        {
            spawnPointPosition = -1;
        }
    }
}
