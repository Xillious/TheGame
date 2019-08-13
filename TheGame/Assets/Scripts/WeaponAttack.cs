using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAttack : MonoBehaviour
{
    private Coroutine attacking;

    private List<GameObject> hitUnits = new List<GameObject>();

    private BoxCollider2D hitBox;

    private Weapon weapon;

    void Start()
    {
        hitBox = GetComponent<BoxCollider2D>();
        weapon = FindObjectOfType<Weapon>();
    }

    public void SwingWeapon()
    {

        // //animation      play weapon animation.
        // //audio          add audio reference in the weapon script

        if (attacking != null)
            StopCoroutine(attacking);

        attacking = StartCoroutine(CRT_Attack());
    }

    private IEnumerator CRT_Attack()
    {
        hitUnits.Clear();
        yield return new WaitForSecondsRealtime(weapon.attackBuildup);
        hitBox.enabled = true;
        yield return new WaitForSecondsRealtime(weapon.attackDuration);
        hitBox.enabled = false;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        /*
        if (other.gameObject.tag == "Enemy" && !hitUnits.Contains(other.gameObject))
        {
            hitUnits.Add(other.gameObject);
            Hit(other.gameObject);
        }
        */

        if (other.gameObject.layer == 10 && !hitUnits.Contains(other.gameObject))
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
}
