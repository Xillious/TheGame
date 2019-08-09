﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon
{
    private BoxCollider2D hitBox;
    

    private List<GameObject> hitUnits = new List<GameObject>();

    public bool canPickup = false;
    private bool weaponFacingRight = true;

   

   
    void Start()
    {
        hitBox = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        
    }

    private Coroutine attacking;

    public void Attack()
    {
   
        // //animation 
        // //audio
        
        if (attacking != null)
            StopCoroutine(attacking);

        attacking = StartCoroutine(CRT_Attack());
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
        if (Input.GetButtonDown("Drop") && thePlayer.hasWeapon)
        {
            DetatchFromParent();
            thePlayer.hasWeapon = false;
            thePlayer.myWeapon = null;
        } 
    }

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
        if (other.CompareTag("Player"))
        {
            canPickup = true;
            thePlayer.weaponInRange = this.gameObject;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canPickup = false;
            thePlayer.weaponInRange = null;
        } 
    }

}
