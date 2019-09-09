using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawnerThree : MonoBehaviour
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

    public float timeBetweenWaves = 5f;
    public float waveCountdown;

    private float searchCountdown = 1f;



    private SpawnState spawnState;

    IEnumerator Start()
    {
        ChangeState(SpawnState.Counting);

        while(true)
        {
            switch(spawnState)
            {
                case SpawnState.Counting:
                    Counting();
                break;

                case SpawnState.Spawning:
                    Spawning();
                break;

                case SpawnState.Waiting:
                    Waiting();
                break;
            }
            yield return new WaitForEndOfFrame();
        }  

    }

    private void Awake()
    {
        waveCountdown = timeBetweenWaves;
    }

    private void Update()
    {
        //Debug.Log(EnemyIsAlive());
    }

    private void Counting()
    {
        //Debug.Log("Counting");
        if (waveCountdown <= 0)
        {
            if (spawnState != SpawnState.Spawning)
            {
                //ChangeState(SpawnState.Spawning);
                StartCoroutine(CRT_SpawnWave(waves[nextWave]));
            }
        } else
        {
            waveCountdown -= Time.deltaTime;
        }
    }

    private void Spawning()
    {
       // Debug.Log("Spawning");
        

    }

    private void Waiting()
    {
        //Debug.Log("Waiting");
        if (!EnemyIsAlive())
        {
            Debug.Log("Wave Compete");
            WaveCompleted();
        } else
        {
            return;
        }
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

    private bool EnemyIsAlive()
    {
        searchCountdown -= Time.deltaTime;

        if (searchCountdown <= 0f)
        {
            searchCountdown = 1f;
            if (GameObject.FindGameObjectsWithTag("Enemy") == null)
            {
                return false;
            }
        }
        return true;
    }

    void SpawnEnemy(Transform enemy)
    {
        //spawn enemy
        Debug.Log("Spawning Enemy: " + enemy.name);
        Instantiate(enemy, transform.position, transform.rotation);
    }

    private void WaveCompleted()
    {

        Debug.Log("WaveCompleted");

        ChangeState(SpawnState.Counting);
        waveCountdown = timeBetweenWaves;

        if (nextWave + 1 > waves.Length - 1)
        {
            nextWave = 0;
        }else
        {
            nextWave++;
        }
    }

    private void ChangeState(SpawnState newState)
    {
        spawnState = newState;
    }
}
