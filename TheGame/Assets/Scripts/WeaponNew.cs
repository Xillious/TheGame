using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponNew : MonoBehaviour
{
    public float damage;
    public float attackBuildup;
    public float attackDuration;
    public float knockback;
    public float knockbackTime;

    public bool canPickup = false;

    public PlayerController thePlayer;

    private BoxCollider2D hitBox;

    private Attack weaponAttack;

    public GameObject weaponIcon;

    public SpriteRenderer icon;

    // Start is called before the first frame update
    void Awake()
    {
        thePlayer = FindObjectOfType<PlayerController>();
        hitBox = GetComponent<BoxCollider2D>();
        weaponAttack = GetComponentInChildren<Attack>();  
        icon = weaponIcon.GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // if the player has no weapon, pick this up
        if (thePlayer.myWeapon == null)
        {
            thePlayer.myWeapon = this.gameObject;
            // do a pickup sound + animation
        }
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

}
