using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour
{

    public int treasureValue;

    public float startTime;
    public float pickupRadius;

    private float time;

    public string soundName;

    public Transform player;

    private Score score;

    private AudioManager audioManager;

    public GameObject scoreText;

    void Start()
    {
        time = startTime;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        score = FindObjectOfType<Score>();

        audioManager = AudioManager.instance;
        if (audioManager == null)
        {
            Debug.LogError("No AudioManager found in scene.");
        }
    }

   
    void Update()
    {
        DestroyTimer();
        Pickup();
    }

    void Pickup()
    {
        if (PlayerRangeCheck(pickupRadius))
        {
            audioManager.PlaySound(soundName);
            score.PickupCoin(treasureValue);
            Destroy(gameObject);
            Instantiate(scoreText, transform.position, transform.rotation);
        }
    }

    public bool PlayerRangeCheck(float distance)
    {
        if (Vector3.Distance(transform.position, player.transform.position) < distance)
            return true;
        else
            return false;
    }

    void DestroyTimer()
    {
        if (time < 0)
        {
            Destroy(gameObject);
        }
        else if (time >= 0)
        {
            time -= Time.deltaTime;
        }
    }

    void LookForPlayer()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, pickupRadius);
    }
}
