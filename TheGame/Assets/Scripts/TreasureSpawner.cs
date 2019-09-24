using System.Collections;
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
        public Transform treasure;
        public int count;
        public float spawnRate;
    }

    public Treasure[] treasures;

    public Transform[] spawnPoints;


    public float countdown;
    public float countdownTime;
    public float waiting;
    public float waitingTime;
                
    

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
        Debug.Log(treasureSpawnState);
    }

    void Counting()
    {
        if (countdown <= 0)
        {
            ChangeState(TreasureSpawnState.Spawning);
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
        Debug.Log("waeCompleted");
    }

    IEnumerator CRT_SpawnTreasure(Treasure treasure)
    {
        ChangeState(TreasureSpawnState.Spawning);

        for (int i = 0; i < treasure.count; i++)
        {
            yield return new WaitForSeconds(treasure.spawnRate);
        }

        ChangeState(TreasureSpawnState.Waiting);
        yield break;
    }

    void SpawnTreasure(Transform treasure)
    {
        Debug.Log("Spawning Treasure");
    }

    private void ChangeState(TreasureSpawnState newState)
    {
        treasureSpawnState = newState;
    }
}
