using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private Coroutine attacking;

    private List<GameObject> hitunits = new List<GameObject>();

    private BoxCollider2D hitBox;

    private Enemy enemy;

    private float test;

    public void SwingWeapon()
    {
        if (attacking != null)
            StopCoroutine(attacking);

        attacking = StartCoroutine(CRT_Attack());
    }

    private IEnumerator CRT_Attack()
    {
        hitunits.Clear();
        yield return new WaitForSecondsRealtime(1);
        {
            hitBox.enabled = true;
        }
        yield return new WaitForSecondsRealtime(1);
        {
            hitBox.enabled = false;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!hitunits.Contains(other.gameObject))
        {
            hitunits.Add(other.gameObject);
            
        }
    }

    private void Hit(GameObject _target)
    {
        if (_target.gameObject.layer == 9)
        {
            _target.GetComponent<PlayerController>().TakeDamage(3);


        }
    }

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
}
