using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blood : MonoBehaviour
{
    private float lifetime;
    private float ActiveTime;
    public float ActiveTimeMin;
    public float ActiveTimeMax;

    public ParticleSystem blood;
    private SpriteRenderer bloodSprite;

    void Start()
    {
        bloodSprite = GetComponent<SpriteRenderer>();
        // ActiveTime = Random.Range(ActiveTimeMin, ActiveTimeMax);
        ActiveTime = .2f;
        lifetime = 3f;
        blood.Play();
    }

   
    void Update()
    {
        ActiveTime -= Time.deltaTime;
        lifetime -= Time.deltaTime;

        if (ActiveTime <= 0)
        {
            bloodSprite.enabled = false;
        }

        if (lifetime <= 0)
        {
            Destroy(gameObject); 
        }
        
       
    }


}
