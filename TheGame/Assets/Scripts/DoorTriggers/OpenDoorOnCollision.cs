using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorOnCollision : MonoBehaviour
{

    private Door door;

    void Start()
    {
        door = FindObjectOfType<Door>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            door.OpenDoor();
        }
    }
}
