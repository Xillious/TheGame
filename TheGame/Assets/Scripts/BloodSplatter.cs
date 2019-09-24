using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodSplatter : MonoBehaviour
{

    ParticleSystem particle;
    public GameObject splatPrefab;
    public Transform splatHolder;
    private List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();
    public AudioSource audioSource;
    public AudioClip[] sounds;
    public float soundCapResetSpeed = 0.5f;
    public int maxSounds = 3;
    float timePassed;
    int soundsPlayed;

    void Start()
    {
        particle = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        timePassed += Time.deltaTime;
        if (timePassed > soundCapResetSpeed)
        {
            soundsPlayed = 0;
            timePassed = 0;
        }
    }
    private void OnParticleCollision(GameObject other)
    {
        ParticlePhysicsExtensions.GetCollisionEvents(particle, other, collisionEvents);

        int count = collisionEvents.Count;
        
        for (int i = 0; i < count; i++)
        {
            Instantiate(splatPrefab, collisionEvents[i].intersection, Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)), splatHolder);
            if (soundsPlayed < maxSounds)
            {
               
            }
        }
    }
}
