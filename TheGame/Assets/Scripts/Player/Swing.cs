using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swing : MonoBehaviour
{

    //get the weapon swing speed "backswing"
    //get the weapon swing speed "downswing"
    //some weapons like an axe will need to speed up halfway into the downswing.

    

    void Update()
    {
        //Debug.Log(transform.rotation);

        if (Input.GetButtonDown("Attack"))
        {
            
        }

        // transform.eulerAngles.z = new Vector3(0, 0, 2);
        // WeaponSwing();
        //transform.rotation = Quaternion.Euler(45f, Mathf.Sin(Time.deltaTime * 2f), 0, 0, 0);
        //transform.rotation = Quaternion.Euler(0, 0, 22f);
    }

   

    private void WeaponSwing()
    {
       // Debug.Log("Swinging");

        if (transform.rotation.z < .3 && transform.rotation.z > .9)
        {
            transform.Rotate(0, 0, .5f);
             
        }
        else if (transform.rotation.z >= .9)
        {
            Debug.Log("DFSADFASDFASDF");
            transform.Rotate(0, 0, -.5f);
        }

    }
}
