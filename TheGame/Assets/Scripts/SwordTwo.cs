using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordTwo : Weapon
{

    private WeaponPickup weaponPickup;
    private WeaponAttack weaponAttack;

    public GameObject weaponIcon;

    private SpriteRenderer icon;

    void Start()
    {
        weaponPickup = FindObjectOfType<WeaponPickup>();
        weaponAttack = FindObjectOfType<WeaponAttack>();
        weaponIcon = GameObject.Find("Icon");
    }


    void Update()
    {

    }

    
    private void Attack()
    {
        weaponAttack.SwingWeapon();
        Debug.Log("Player Attacking");
    }

    private void Pickup()
    {
        weaponPickup._Pickup();
        Debug.Log("Pickup Weapon");
    }

    private void Drop()
    {
        weaponPickup._Drop();
    }
    
}
