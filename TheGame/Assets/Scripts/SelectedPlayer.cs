using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedPlayer : MonoBehaviour
{

    GameMaster gameMaster;

    private Image selectedPlayer;

    

    void Start()
    {
        gameMaster = FindObjectOfType<GameMaster>();
        
    }

    
    void Update()
    {
        gameMaster.player.GetComponent<SpriteRenderer>();
        selectedPlayer = gameMaster.player.GetComponent<Image>();
    }
}
