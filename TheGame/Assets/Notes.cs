using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*

   



    if certain score is met activate item

    ony at the time a score is met
        eg if it is 100 its active but if its 101 its not

    if score = an amount and time is greater or less than an amount


    if score is greater than on amount and less than another amount



    new enemies 
    new weapons? 

    showcase
        only play for a couple of minutes
        quick reset so not opening and cosing the game.

    highscore level
        single level where enemies and treasure endlessly spawn?




    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateScoreExact : MonoBehaviour
{

    public GameObject item;

    public int requiredScore;

    private Score score;

    void Start()
    {
        score = FindObjectOfType<Score>();
    }

    // Update is called once per frame
    void Update()
    {
        ShowItem(requiredScore);
    }

    void ShowItem(int reqScore)
    {
        if (score.score == reqScore)
        {
            item.SetActive(true);
        }
    }
}

































    - the health bar on the enemies that spawn isnt showing up because they dont recognise the player (target) position
        - also why they dont chase


    Colour Codes Background
     
    foreground
     dakk - light
     - 15141a
     - 333140
     - 514e66
     - 6f6b8c


    background
     - 000000
     - 020203
     - 0a0a0d












   - make enemy prefab
   - test the scene with multiple enemies
   - make player prefab

   - make a health bar for the enemies
   

   - the enemy needs to detect the players transform when the player moves in range



    - make a health bar for the player
        - attatch it to the player
        - make it smoother





     - add the platform ledge so the player can pull themself up.
    - dont worry about flipping the player for now
    - work on the rest of the enemy AI
    - build the first level
    - look at how to do the loot system? - is there a loot system?
   


        rb.velocity doesnt change apparently?
        the flip works just need to figure out when to apply it.



    how to get the player.transform without a seperate trigger?

    change the players box collider when crouching 
    but make it so they cant stand in small gaps?
        maybe just dont have any small gaps? crouch only for dodging?


    if you bump into the enemy you take some small amount of damage but if they hit you you with their attack they do lots.

    enemy states
        do i need a knockback state?
            if in this state the movement will be turned off so the knockback works?
            maybe this can just be doin in the knockback trigger?

        do i need seperate states for idle standin still and idle wandering around?
            maybe in the Idle() function it chooses to either stand still or move around
            i think idle will handle all of this and just choose randomly what the enemy should do in this time
            jumping?

        seperate states for idle and looking for the player?
            searching state?

        if the player knocks back the enemy does it reset the attack of the enemy?
            can stun lock the enemies?

        
    maybe the enemy aggro range and the "aggro dropoff" should be two seperate values?



    weapon special attack
    power bar when its full you can perform a special attack
    if (bar is full && left trigger held && attack button pressed)
        do special

    health potions pick up automatically and stack?
    rb to use healthe pot

    coins spew out of dead enemies like blood?

    shop (if have time)
    a break inbetween levels where you can buy

    multiple options with a coin cost benith
    if (player is standing on weapon to buy && coins >= cost)
        swap weapon with one equipped
        coins - cost

    switch statement to decide what the idle should be like rolling a d100 and looking at a chart.

    eg 1-20 walk around
      20-60 stand still
      60-70 walk and jump
      70-100 something else
      decide what to do when they enter the idle state.


    packages 
    A* Pathfinding 


    if enemy x is less than player x 
    apply negative force
    and flip sprite.

    transform.position < target.transform.position
    {
    do stuff
    }

    Vector2.distance

    can make the enemy slow down or speed up as it gets closer.

    scale weapon based on kills
    gets bigger as you get more kills 

    enemy walking on a sine wave.



    CTRL + T - search for a lin in visual studio.
    Hold alt to move a line up or down
    CTRL + R - change all variable names at once

    https://forum.unity.com/threads/c-proper-state-machine.380612/

*/
