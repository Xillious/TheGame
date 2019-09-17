using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
   
    public enum SpawnState
    {
        Spawning,
        Waiting,
        Counting
    }

    [System.Serializable]
    public class Wave
    {
        public string name;
        public Transform enemy;
        public int count;
        public float spawnRate;
    }

    public Wave[] waves;
    private int nextWave = 0;

    public Transform[] spawnPoints;

    public float timeBetweenWaves = 5f;
    public float waveCountdown;

    private float searchCountdown = 1f;

    private SpawnState spawnState = SpawnState.Counting;

    void Start()
    {
        waveCountdown = timeBetweenWaves;
        

        if (spawnPoints.Length == 0)
        {
            Debug.Log("No spawn points referneced");
        }
    }

    void Update()
    {
        if (spawnState == SpawnState.Waiting)
        {

            //check if enemys are still alive
            if (!EnemyIsAlive())
            {
                //begin new round
                WaveCompleted();
                //return;
            } else
            {
                return;
            }
        }

       // Debug.Log(EnemyIsAlive());
        //Debug.Log(spawnState);

        if (waveCountdown <= 0)
        {
            if (spawnState != SpawnState.Spawning)
            {
                //start spawning wave
                StartCoroutine(CRT_SpawnWave(waves[nextWave]));

            }
        } else
        {
            waveCountdown -= Time.deltaTime;
        }
    }

    private void WaveCompleted()
    {
        Debug.Log("Wave Completed");
        ChangeState(SpawnState.Counting);

        if (nextWave + 1 > waves.Length - 1)
        {
            nextWave = 0;
            Debug.Log("All Waves Complete  Looping");
        }
        else
        {
            nextWave++;
        }
    }

    bool EnemyIsAlive()
    {
        searchCountdown -= Time.deltaTime;
        if (searchCountdown <= 0)
        {
            searchCountdown = 1f;
            if (GameObject.FindGameObjectsWithTag("Enemy") == null)
            {
                return false;
            }
        }
        return true;
    }

    IEnumerator CRT_SpawnWave(Wave wave)
    {
        Debug.Log("Spawning Wave:" + wave.name);

        ChangeState(SpawnState.Spawning);
        
        for (int i = 0; i < wave.count; i++)
        {
            SpawnEnemy(wave.enemy);
            yield return new WaitForSeconds(1f / wave.spawnRate);
        }
        
        ChangeState(SpawnState.Waiting);
        yield break;
    }

   

    

    private void ChangeState(SpawnState newState)
    {
        spawnState = newState;
    }

    void SpawnEnemy(Transform enemy)
    {
        //spawn enemy
        Debug.Log("Spawning Enemy: " + enemy.name);
        Instantiate(enemy, transform.position, transform.rotation);
    }
}
