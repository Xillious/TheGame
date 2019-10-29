using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondSpawn : MonoBehaviour
{

    Score score;

    int diamondsCount;

    public GameObject diamond;

    void Start()
    {
        score = FindObjectOfType<Score>();
    }

    // Update is called once per frame
    void Update()
    {
       

        if (Diamond(30, 40) & diamondsCount == 0)
        {
            SpawnDiamond();
        }
        if (Diamond(60, 70) & diamondsCount == 1)
        {
            SpawnDiamond();
        }
        if (Diamond(90, 100) & diamondsCount == 2)
        {
            SpawnDiamond();
        }
        if (Diamond(120, 130) & diamondsCount == 3)
        {
            SpawnDiamond();
        }
        if (Diamond(150, 160) & diamondsCount == 4)
        {
            SpawnDiamond();
        }
        if (Diamond(180, 190) & diamondsCount == 5)
        {
            SpawnDiamond();
        }
        if (Diamond(210, 220) & diamondsCount == 6)
        {
            SpawnDiamond();
        }
        if (Diamond(240, 250) & diamondsCount == 7)
        {
            SpawnDiamond();
        }
        if (Diamond(270, 280) & diamondsCount == 8)
        {
            SpawnDiamond();
        }
        if (Diamond(300, 310) & diamondsCount == 9)
        {
            SpawnDiamond();
        }
        if (Diamond(330, 340) & diamondsCount == 10)
        {
            SpawnDiamond();
        }
        if (Diamond(360, 370) & diamondsCount == 11)
        {
            SpawnDiamond();
        }
        if (Diamond(390, 400) & diamondsCount == 12)
        {
            SpawnDiamond();
        }
        if (Diamond(420, 430) & diamondsCount == 13)
        {
            SpawnDiamond();
        }
        if (Diamond(450, 460) & diamondsCount == 14)
        {
            SpawnDiamond();
        }
    }

    bool Diamond(int min, int max)
    {
        if (score.kills > min && score.kills < max)
        {
            return true;
        }
        return false;
    }

    void SpawnDiamond()
    {
        
         Instantiate(diamond, transform.position, transform.rotation);
         diamondsCount++;
        
    }
}
