using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyOnLoad : MonoBehaviour
{
    GameMaster gameMaster;

    private void Awake()
    {
        gameMaster = FindObjectOfType<GameMaster>();

        DontDestroyOnLoad(transform.gameObject);
        Scene scene = SceneManager.GetActiveScene();

        
    }
    private void Start()
    {
       
    }
    private void Update()
    {
        if (gameMaster.gameOver == true)
        {
            Destroy(gameObject);
        }
    }
}
