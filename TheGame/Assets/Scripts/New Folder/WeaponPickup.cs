using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : Sword
{

    public void _Pickup()
    {
        SetParent(thePlayer.weaponPosition);
        canPickup = false;
        thePlayer.hasWeapon = true;
        transform.rotation = thePlayer.transform.rotation;
    }

    public void _Drop()
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
