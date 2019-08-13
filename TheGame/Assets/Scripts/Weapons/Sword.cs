using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon
{
    public bool canPickup = false;
    private bool weaponFacingRight = true;

    private BoxCollider2D hitBox;
   
    private List<GameObject> hitUnits = new List<GameObject>();

    public GameObject weaponIcon;

    private SpriteRenderer icon;

    private Animator anim;

    private WeaponAttack weaponAttack;

    private Coroutine attacking;

    void Start()
    {
        hitBox = GetComponent<BoxCollider2D>();
        weaponIcon = GameObject.Find("Weapon Icon");
        icon = weaponIcon.GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        weaponAttack = FindObjectOfType<WeaponAttack>();
    }

    void Update()
    {
        
        
    }

    public void Attack()
    {
        weaponAttack.SwingWeapon();
    }

    /*
    public void Attack()
    {
   
        // //animation 
        // //audio
        
        if (attacking != null)
            StopCoroutine(attacking);

        attacking = StartCoroutine(CRT_Attack());
    }
    */

    public void Pickup()
    {   
        SetParent(thePlayer.weaponPosition);
        canPickup = false;
        thePlayer.hasWeapon = true;
        transform.rotation = thePlayer.transform.rotation;
    }

    public void Drop()
    {
        if (Input.GetButtonDown("Drop") && thePlayer.hasWeapon)
        {
            DetatchFromParent();
            thePlayer.hasWeapon = false;
            thePlayer.myWeapon = null;
        } 
    }

    /*
    private IEnumerator CRT_Attack()
    {
        hitUnits.Clear();
        yield return new WaitForSecondsRealtime(attackBuildup);
        hitBox.enabled = true;
        yield return new WaitForSecondsRealtime(attackDuration);
        hitBox.enabled = false;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
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
    */

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
        /*
        if (other.CompareTag("Player"))
        {
            canPickup = true;
            thePlayer.weaponInRange = this.gameObject;
            icon.enabled = true;
        }
        */

        if (other.gameObject.layer == 9)
        {
            Debug.Log("Colliding with player");
            canPickup = true;
            thePlayer.weaponInRange = this.gameObject;
            icon.enabled = true;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        /*
        if (other.CompareTag("Player"))
        {
            canPickup = false;
            thePlayer.weaponInRange = null;
            icon.enabled = false;
        } 
        */

        if (other.gameObject.layer == 9)
        {
            canPickup = false;
            thePlayer.weaponInRange = null;
            icon.enabled = false;
        }
    }

}
