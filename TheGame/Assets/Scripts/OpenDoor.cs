using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{

    private SpriteRenderer rend;
    public Sprite doorOpen, doorCloded;

    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        rend.sprite = doorCloded;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            rend.sprite = doorOpen;
        }
    }
}
