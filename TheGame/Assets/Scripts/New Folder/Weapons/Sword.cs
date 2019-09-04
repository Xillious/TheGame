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
        //weaponAttack = FindObjectOfType<WeaponAttack>();
        weaponAttack = GetComponentInChildren<WeaponAttack>();
    }

    void Update()
    {
        
        
    }

    public void Attack()
    {
        weaponAttack.SwingWeapon();
    }

    public void Pickup()
    {   
        SetParent(thePlayer.weaponPosition);
        canPickup = false;
        thePlayer.hasWeapon = true;
        transform.rotation = thePlayer.transform.rotation;
    }

    public void Drop()
    {
        DetatchFromParent();
        thePlayer.hasWeapon = false;
        thePlayer.myWeapon = null;
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
 
        if (other.gameObject.layer == 9)
        {
            canPickup = true;
            thePlayer.weaponInRange = this.gameObject;
            icon.enabled = true;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
       
        if (other.gameObject.layer == 9)
        {
            canPickup = false;
            thePlayer.weaponInRange = null;
            icon.enabled = false;
        }
    }

}
