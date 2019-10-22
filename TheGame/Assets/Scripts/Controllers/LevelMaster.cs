using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMaster : MonoBehaviour
{
    public int levelTime = 60;
    public int[] increaseSpawnRateAt;

    public float[] increasedSpawnRate;

    private Timer timer;

    private Score score;
    private NextLevelScore nextLevelScore;

    public GameObject doorObject;
    public GameObject waveSpawnerObject;
    public GameObject treasureSpawnerObject;

    private WaveSpawner waveSpawner;
    private TreasureSpawner treasureSpawner;

    private Door door;

    public int requiredScore;

    private void Awake()
    {
        timer = FindObjectOfType<Timer>();
        nextLevelScore = FindObjectOfType<NextLevelScore>();
    }
    void Start()
    {
        timer.SetStartTime(levelTime);
        Time.timeScale = 1f;
        score = FindObjectOfType<Score>();
        door = doorObject.GetComponent<Door>();
        waveSpawner = waveSpawnerObject.GetComponent<WaveSpawner>();
        treasureSpawner = treasureSpawnerObject.GetComponent<TreasureSpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        UnlockNextLevel(requiredScore);

        //Debug.Log(waveSpawner.waves[0].spawnRate);

       // Debug.Log(nextLevelScore.requiredScore);

        nextLevelScore.requiredScore = requiredScore;

        

        if (score.score > increaseSpawnRateAt[0] && score.score < increaseSpawnRateAt[1])
        {
            waveSpawner.waves[0].spawnRate = increasedSpawnRate[0];
        }
         else if (score.score > increaseSpawnRateAt[1] && score.score < increaseSpawnRateAt[2])
        {
            waveSpawner.waves[0].spawnRate = increasedSpawnRate[1];
        }
        else if (score.score > increaseSpawnRateAt[2] && score.score < increaseSpawnRateAt[3])
        {
            waveSpawner.waves[0].spawnRate = increasedSpawnRate[2];
        }

         
    }

    void UnlockNextLevel(int reqScore)
    {
        if (score.score >= reqScore)
        {
            if (doorObject != null)
            {
                doorObject.SetActive(true);
                door.OpenDoor();
            }
        }
    }

    void IncreaseSpawnRate(float newSpawnRate)
    {
        waveSpawner.waves[0].spawnRate = newSpawnRate;
    }

    void ScoreToIncreaseAt(int _score)
    {
        if (score.score >= _score)
        {
            IncreaseSpawnRate(increasedSpawnRate[0]);
        }
    }

    
}
