using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public string nextLevel;

    private float doorHitbox;
    public float doorhitboxSize;

    public bool doorIsOpen;

    public Transform player;

    private SpriteRenderer rend;

    public Sprite doorOpen, doorClosed;

    private GameObject playerGameobject;

    private AudioManager audioManager;

    void Start()
    {

        rend = GetComponent<SpriteRenderer>();
        rend.sprite = doorClosed;
        // player = GameObject.Find("Player").transform;
        //player.transform.position = gameMaster.player.transform.position;
        //playerGameobject = gameMaster.player;
        //player = GameObject.FindWithTag("Player").transform;
        //player = GameObject.FindGameObjectWithTag("Player").transform;

        StartCoroutine(FindPlayer());


        audioManager = AudioManager.instance;
        if (audioManager == null)
        {
            Debug.LogError("No AudioManager found in scene.");
        }
    }

    private IEnumerator FindPlayer()
    {
        yield return new WaitForSecondsRealtime(0.2f);
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }


    void Update()
    {

        if (PlayerRangeCheck(doorHitbox))
        {
            audioManager.PlaySound("DoorSound");
            NextLevel(nextLevel);
        }


    }

    public void OpenDoor()
    {
        rend.sprite = doorOpen;
        doorHitbox = doorhitboxSize;
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
        if (player != null)
        {
            
            if (Vector3.Distance(transform.position, player.transform.position) < distance)
                return true;
            else
                return false;
        }
        else
            return false;
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, doorHitbox);
    }
}
