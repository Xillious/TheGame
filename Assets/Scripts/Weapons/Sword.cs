using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon
{
    private BoxCollider2D hitBox;
    private List<GameObject> hitUnits = new List<GameObject>();
   

   public bool canPickup = false;
    private bool isFacingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        hitBox = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canPickup && Input.GetButtonDown("Pickup") && thePlayer.hasWeapon == false)
        {
            transform.SetParent(thePlayer.transform);
            canPickup = false;
            thePlayer.hasWeapon = true;
            //change the player myWeapon reference.
           thePlayer.myWeapon = this.gameObject;

           
            if (thePlayer.transform.localEulerAngles.y != 0)
            {
                //Flip();
                Debug.Log("Flip Weapon");
            }
            
        }

        if (Input.GetButtonDown("Drop") && thePlayer.hasWeapon == true)
        {
            DetatchFromParent();
            thePlayer.hasWeapon = false;
        }
    }

    private Coroutine attacking;

    public void Attack()
    {
        // Debug.Log("Doing an attack");
        // //enable weapon collider
        // //animation 
        // //audio
        
        if (attacking != null)
            StopCoroutine(attacking);

        attacking = StartCoroutine(CRT_Attack());
    }

    private IEnumerator CRT_Attack()
    {
        hitUnits.Clear();
        hitBox.enabled = true;
        yield return new WaitForSecondsRealtime(attackDuration);
        hitBox.enabled = false;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        //Debug.Log("Hit " + other.gameObject.name + " at time " + Time.time.ToString());

        if (other.gameObject.tag == "Enemy" && !hitUnits.Contains(other.gameObject))
        {
            hitUnits.Add(other.gameObject);
            Hit(other.gameObject);
        }
    }

    //damage enemy
    private void Hit(GameObject _target)
    {
        Debug.Log("HIT ENEMY");
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
        //need to add pickup key.  &&  wasnt working
        if (other.CompareTag("Player"))
        {
            canPickup = true;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canPickup = false;
        }
    }

    private void Flip()
    {
        
        transform.Rotate(0, 180, 0);
    }
}
