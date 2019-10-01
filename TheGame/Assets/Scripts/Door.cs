using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public string nextLevel;

    private float doorHitbox;

    public bool doorIsOpen;

    private Transform player;

    private SpriteRenderer rend;

    public Sprite doorOpen, doorClosed;

    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        rend.sprite = doorClosed;
        player = GameObject.Find("Player").transform;
    }

    
    void Update()
    {
       
        if (PlayerRangeCheck(doorHitbox))
        {
            Debug.Log("DOOR");
            NextLevel(nextLevel);
        }
    }

    public void OpenDoor()
    {
        rend.sprite = doorOpen;
        doorHitbox = 0.5f;
        //opening sound?
    }

    public void CloseDoor()
    {
        rend.sprite = doorClosed;
        doorHitbox = 0f;
        //clodingSound?
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
