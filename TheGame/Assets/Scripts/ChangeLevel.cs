using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeLevel : MonoBehaviour
{
    //SceneManager.LoadScene("LevelOneEasy");

    
    public string nextLevel;

    public float doorHitbox = 1;

    private Transform player;

    void Start()
    {
        player = GameObject.Find("Player").transform;
    }

    
    void Update()
    {
        if (PlayerRangeCheck(doorHitbox))
        {
            Debug.Log("DOOR");
        }
    }

    void NextLevel(string nextLevel)
    {
        SceneManager.LoadScene(nextLevel);
    }

    public bool PlayerRangeCheck(float distance)
    {
        if (Vector3.Distance(transform.position, player.transform.position) < distance)
            return true;
        else
            return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, doorHitbox);
    }

}
