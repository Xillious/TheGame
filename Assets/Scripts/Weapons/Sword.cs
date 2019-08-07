using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon
{
    private BoxCollider2D hitBox;
    public GameObject player;
    private List<GameObject> hitUnits = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        hitBox = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {

    
    }

    public void Attack()
    {
       // Debug.Log("Doing an attack");
       // //enable weapon collider
       // //animation 
       // //audio

        StartCoroutine(CRT_Attack());
    }

    private IEnumerator CRT_Attack()
    {
        hitUnits.Clear();
        hitBox.enabled = true;
        float _attackDuration = 0.5f;
        while (_attackDuration > 0)
        {
             _attackDuration -= Time.deltaTime;
            yield return null;
        }
        hitBox.enabled = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "enemy" && hitUnits.Contains(other.gameObject))
        {
            hitUnits.Add(other.gameObject);
            Hit(other.gameObject);
        }
    }

    //damage enemy
    private void Hit(GameObject _target)
    {
        
    }

    private void SetParent(GameObject newParent)
    {
        transform.SetParent(newParent.transform);
        transform.localPosition = Vector3.zero;
    }

    private void DetatchFromParent()
    {
        transform.parent = null;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        //need to add pickup key.  && Input.GetButtonDown("Pickup") wasnt working
        if (other.CompareTag("Player"))
        {
            SetParent(player);
            Debug.Log("Hello there");
        }
    }
}
