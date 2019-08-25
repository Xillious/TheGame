using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAttack : MonoBehaviour
{
    private Coroutine attacking;

    private List<GameObject> hitUnits = new List<GameObject>();

    private BoxCollider2D hitBox;

    private Weapon weapon;

    private SpriteRenderer hitboxSpriteTest;

    void Start()
    {
        hitBox = GetComponent<BoxCollider2D>();
        weapon = FindObjectOfType<Weapon>();
        hitboxSpriteTest = GetComponent<SpriteRenderer>();
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
        {
            hitBox.enabled = true;
            hitboxSpriteTest.enabled = true;
        }
        yield return new WaitForSecondsRealtime(weapon.attackDuration);
        {
            hitBox.enabled = false;
            hitboxSpriteTest.enabled = false;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        
        if (!hitUnits.Contains(other.gameObject))
        {
            hitUnits.Add(other.gameObject);
            Hit(other.gameObject);
        }
    }

    //damage enemy
    private void Hit(GameObject _target)
    {
        if (_target.gameObject.layer == 10)
        {
            //deal damage
            _target.GetComponent<Enemy>().TakeDamage(weapon.damage);

            //knockback enemy
            Rigidbody2D enemy = _target.GetComponent<Rigidbody2D>();
            if (enemy != null)
            {
                enemy.isKinematic = false;
                Vector2 difference = enemy.transform.position - transform.position;
                //difference = difference.normalized * weapon.knockback;
                //enemy.AddForce(difference, ForceMode2D.Impulse);
                StartCoroutine(CRT_Knockback(enemy));

                //knockback direction
                if ( _target.GetComponent<Enemy>().isFacingRight)
                {
                    enemy.AddForce(new Vector3(1, 1, 0) * weapon.knockback, ForceMode2D.Impulse);
                } else if (!_target.GetComponent<Enemy>().isFacingRight)
                {
                    enemy.AddForce(new Vector3(-1, 1, 0) * weapon.knockback, ForceMode2D.Impulse);
                }
            }
        }
    }
        private IEnumerator CRT_Knockback(Rigidbody2D enemy)
    {
        if (enemy != null)
        {
            yield return new WaitForSeconds(weapon.knockbackTime);
            enemy.velocity = Vector2.zero;
            //enemy.isKinematic = true;
        }
    }
}
