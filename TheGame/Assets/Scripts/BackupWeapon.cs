using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackupWeapon : MonoBehaviour
{
    public GameObject[] backupWeapon;
    private PlayerController playerController;

    void Start()
    {
       
        playerController = FindObjectOfType<PlayerController>();

        if (playerController.myWeapon == null)
        {
            Debug.Log("Player has no weapon");
            Instantiate(backupWeapon[Random.Range(0, backupWeapon.Length)], transform.position, transform.rotation);
        }
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
