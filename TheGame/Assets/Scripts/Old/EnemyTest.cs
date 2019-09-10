using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTest : MonoBehaviour
{
    private int aiState = 0; // 0 = normal | 1 = aggro
    private float cooldown_max = 120, cooldown_current = 0;
    private GameObject target;

    public float Int_Health;


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        switch (aiState)
        {
            case 0: // normal
                    // idle actions

                break;
            case 1: // aggro
                    // aggro actions
                    /*
                     * if (Vector2.Distance(transform.position, target.transform.position) > 2){
                     * transform.position = Vector2.MoveTowards(transform.position, target.transform.position, moveSpeed);
                     * }
                     * else if (Vector2.Distance() <= 2){
                     * Attack();
                     * }
                     */
                break;
        }


    }

    public void TakeDamage(float damage)
    {
        //hitpoints -= damage
        //check if dead.
    }

    private void OnTriggerEnter(Collider other)
    {
        /*
		 * if (other.collider.tag == "unit" && other.collider.gameObject.GetComponent<unitScript>().faction != faction{
		 * target = other.collider.gameObject;
		 * aiState = 1;
		 * }
		 */
    }
}
