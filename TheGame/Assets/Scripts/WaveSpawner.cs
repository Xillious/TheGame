using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
   
    public enum SpawnState
    {
        Spawning,
        Waiting,
        Counting,
        Finished
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
    public float waitingTime;
    public float timeUntilNextWave;

    private float searchCountdown = 1f;

    private SpawnState spawnState = SpawnState.Counting;

    private IEnumerator Start()
    {
        waveCountdown = timeBetweenWaves;
        waitingTime = timeUntilNextWave;

        if (spawnPoints.Length == 0)
        {
            Debug.Log("No spawn points referneced");
        }

        while (true)
        {
            switch (spawnState)
            {
                case SpawnState.Counting:
                    //run counting
                    Counting();
                    break;

                case SpawnState.Spawning:
                    //run spawning
                    Spawning();
                    break;

                case SpawnState.Waiting:
                    //run waiting
                    Waiting();
                    break;

                case SpawnState.Finished:
                    //rin finished
                    Finished();
                    break;
                    
            }
            yield return new WaitForEndOfFrame();
        }
    }

    void Update()
    {
        if (spawnState == SpawnState.Counting)
        {
            //Debug.Log("Counting");
        }


        //Debug.Log(spawnState);

        //Debug.Log(EnemyIsAlive());

        //EnemyIsAlive();

        if (GameObject.FindGameObjectsWithTag("Enemy") == null)
        {
            Debug.Log("No Enemies");
        }

        if (spawnState == SpawnState.Waiting)
        {

            //check if enemys are still alive
            if (!EnemyIsAlive())
            {
                //begin new round
               // WaveCompleted();
                //return;
            } else
            {
                return;
            }
        }

       
    }
    void Counting()
    {
        //count down to spawn the next wave
        
        if (waveCountdown <= 0)
        {
            if (spawnState != SpawnState.Spawning)
            {
                StartCoroutine(CRT_SpawnWave(waves[nextWave]));
                ChangeState(SpawnState.Spawning);
            }
        } else if (waveCountdown > 0)
        {
            waveCountdown -= Time.deltaTime;
        }
    }

    void Spawning()
    {
        
    }

    void Waiting()
    {
        
        if (waitingTime <= 0)
        {
            //ChangeState(SpawnState.Counting);
            WaveCompleted();
        }
        else if (waitingTime > 0)
        {
            waitingTime -= Time.deltaTime;
        }
    }

    void Finished()
    {
        Debug.Log("All Waves Complete");
    }

    private void WaveCompleted()
    {
        Debug.Log("Wave Completed");
        ChangeState(SpawnState.Counting);

        waveCountdown = timeUntilNextWave;
        waitingTime = timeBetweenWaves;

        if (nextWave + 1 > waves.Length - 1)
        {
            //nextWave = 0;
            //Debug.Log("All Waves Complete  Looping");
            ChangeState(SpawnState.Finished);
        }
        else
        {
            nextWave++;
        }
    }

    bool EnemyIsAlive()
    {
        searchCountdown -= Time.deltaTime;
        //Debug.Log(searchCountdown);
        if (searchCountdown <= 0)
        {
            searchCountdown = 1f;

            
            /*
            if (GameObject.FindGameObjectsWithTag("Enemy") == null)
            {
                Debug.Log("NO ENEMIES");
                return false;
            }
            */
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
