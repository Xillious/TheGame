using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swing : MonoBehaviour
{

    //get the weapon swing speed "backswing"
    //get the weapon swing speed "downswing"
    //some weapons like an axe will need to speed up halfway into the downswing.

    float rotation;

    [SerializeField]
    //private bool swinging;

    private float min = 1;
    private float max = 50;

    [SerializeField]
    float degrees = 90f;

    private Coroutine swinging;

    private WeaponNew weapon;

    void Update()
    {
        //Debug.Log(transform.rotation);

        weapon = GetComponentInChildren<WeaponNew>();

        if (Input.GetButton("Attack"))
        {
            //SwingNew();
            WeaponSwing();
            
        }
        
        rotation = transform.rotation.z * 100;

        if (weapon != null)
        {
            //Debug.Log(weapon.damage);
        }
        
        //StartCoroutine(CRT_Swinging());


        //Debug.Log(transform.eulerAngles);

        //from rotation, to rotation, speed
        //Debug.Log(rotation);



        // transform.eulerAngles.z = new Vector3(0, 0, 2);
        // WeaponSwing();
        //transform.rotation = Quaternion.Euler(45f, Mathf.Sin(Time.deltaTime * 2f), 0, 0, 0);
        //transform.rotation = Quaternion.Euler(0, 0, 22f);
    }

    private IEnumerator CRT_Swinging()
    {
       // yield return new WaitForSecondsRealtime(0f);
        Vector3 top = new Vector3(0, 0, degrees);
        transform.eulerAngles = Vector3.Lerp(transform.rotation.eulerAngles, top, 12f * Time.deltaTime);
        //yield return new WaitForSecondsRealtime(1f);
        Vector3 bottom = new Vector3(0, 0, -90);
        transform.eulerAngles = Vector3.Lerp(transform.rotation.eulerAngles, bottom, 4f * Time.deltaTime);
        //is either still tryiny to lerp when the z value is at 90.00001 
        // or the second part isnt working at all.
        yield return new WaitForSecondsRealtime(0f);
    }

    private void WeaponSwing()
    {
        if (swinging != null)
            StopCoroutine(swinging);

        swinging = StartCoroutine(CRT_Swinging());
    }

    private void SwingNew()
    {
        Vector3 to = new Vector3(0, 0, degrees);
        transform.eulerAngles = Vector3.Lerp(transform.rotation.eulerAngles, to, 20f * Time.deltaTime);
    }
   
    /*
    private void WeaponSwing()
    {
        if (rotation < max && rotation > min)
        {
            transform.Rotate(0, 0, 5f);
            
        }
        else if (rotation > max && rotation < min)
        {
            transform.Rotate(0, 0, -7f);
           
           // StartCoroutine(CRT_Swinging());
        }
    }
    */
    // 
    // 
}
