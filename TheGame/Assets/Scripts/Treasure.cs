using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour
{

    public int treasureValue;

    public float startTime;
    private float time;

    public float pickupRadius;

    public Transform player;

    private Score score;

    void Start()
    {
        time = startTime;
        player = GameObject.Find("Player").transform;
        score = FindObjectOfType<Score>();
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
            //play sound
            score.PickupCoin(treasureValue);
            Destroy(gameObject);
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

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, pickupRadius);
    }
}
