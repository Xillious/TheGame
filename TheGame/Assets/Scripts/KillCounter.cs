using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KillCounter : MonoBehaviour
{
    public int killCount;
    Text kills;

    void Start()
    {
        kills = GetComponent<Text>();
    }

    void Update()
    {
        kills.text = "Kills: " + killCount;

        if (Input.GetKeyDown(KeyCode.X))
        {
            
        }
    }

    public void EnemyKilled()
    {
        killCount = killCount + 1;
    }
}
