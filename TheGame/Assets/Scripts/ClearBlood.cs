using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearBlood : MonoBehaviour
{

    public float lifeTime;
    private float startTime;
    private SpriteRenderer sprite;
    private SpriteMask mask;
    Color colour;
    bool dying;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        mask = GetComponent<SpriteMask>();
        if (sprite)
        {
            colour = sprite.color;
            startTime = Time.time;
        }
        
    }

    private void Update()
    {
       if (Time.time - startTime > lifeTime && !dying)
        {
            dying = true;
            StartCoroutine(CRT_FadeOut());
        }

        

    }

    IEnumerator CRT_FadeOut()
    {
        float value = 1;
        while (value > 0)
        {
            value -= Time.deltaTime * 0.05f;
            if (sprite)
            {
                sprite.color = new Color(colour.r, colour.g, colour.b, value);
                transform.localScale = new Vector3(value, value, value);
                yield return new WaitForEndOfFrame();
            }
            Destroy(gameObject);
        }
    }

    private void StartFading()
    {
        StartCoroutine(CRT_FadeOut());
    }
}
