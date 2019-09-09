using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawnerTest : MonoBehaviour
{

    public enum SpawnState
    {
        SPAWNNIG,
        WAITING,
        COUNTING
    }

    [System.Serializable]
    public class Wave
    {
        public string name;
        public Transform enemy;
        public int count;
        public float rate;

    }

    public Wave[] waves;
    private int nextWave = 0;

    public Transform[] spawnPoints;

    public float timeBetweenWaves = 5f;
    private float waveCountdown;

    private float searchCountdown = 1f;

    private SpawnState spawnState = SpawnState.COUNTING;

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
        if (spawnState == SpawnState.WAITING)
        {
            if (!EnemyIsAlive())
            {
                WaveCompleted();
            }
            else
            {
                return;
            }
        }

        //Debug.Log(EnemyIsAlive());

        if (waveCountdown <= 0)
        {
            if (spawnState != SpawnState.SPAWNNIG)
            {
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }

    }

    void WaveCompleted()
    {
        Debug.Log("wave Completed");

        //spawnState = SpawnState.COUNTING;
        ChangeState(SpawnState.COUNTING);
        waveCountdown = timeBetweenWaves;

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
        if (searchCountdown <= 0f)
        {
            searchCountdown = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return false;
            }
        }
        return true;
    }

    IEnumerator SpawnWave(Wave _wave)
    {
        Debug.Log("Spawning Wave " + _wave.name);
        //spawnState = SpawnState.SPAWNNIG;
        ChangeState(SpawnState.SPAWNNIG);

        for (int i = 0; i < _wave.count; i++)
        {
            SpawnEnemy(_wave.enemy);
            yield return new WaitForSeconds(1f / _wave.rate);
        }

        //spawnState = SpawnState.WAITING;
        ChangeState(SpawnState.WAITING);

        yield break;

    }

    void SpawnEnemy(Transform enemy)
    {
        Debug.Log("spawning enemy" + enemy.name);
        Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(enemy, _sp.position, _sp.rotation);
    }

    private void ChangeState(SpawnState newState)
    {
        spawnState = newState;
    }

}
