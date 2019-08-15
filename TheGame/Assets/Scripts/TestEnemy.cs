using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : Enemy
{

    private float health;

    void Start()
    {
        InitialiseVariables();
    }
   
    void Update()
    {
        
    }

    private void InitialiseVariables()
    {
            health = Int_Health;
    }
}
