using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHand : MonoBehaviour
{

    public SpriteRenderer handSprite;

    void Awake()
    {
        handSprite = GetComponent<SpriteRenderer>();
    }

  
}
