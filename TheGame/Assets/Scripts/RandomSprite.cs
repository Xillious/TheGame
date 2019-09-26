using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSprite : MonoBehaviour
{

    public float sizeMin;
    public float sizeMax;

    private SpriteRenderer rend;

    public Sprite[] sprites;

   

    void Start()
    {
        Regen();

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Regen()
    {
        rend = GetComponent<SpriteRenderer>();
        int randomSprite = Random.Range(0, sprites.Length);
        float randomSize = Random.Range(sizeMin, sizeMax);

        rend.sprite = sprites[randomSprite];
        transform.localScale = new Vector3(randomSize, randomSize, 0f);

    }
}
