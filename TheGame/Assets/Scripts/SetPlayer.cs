using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPlayer : MonoBehaviour
{

    private GameMaster gameMaster;
    public GameObject player;

    void Start()
    {
        gameMaster = FindObjectOfType<GameMaster>();
    }

    public void SelectCharacter()
    {
        gameMaster.player = player;
    }
}
