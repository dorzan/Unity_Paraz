using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerManager;

public class LobbyManager : MonoBehaviour
{

    PlayerManager playerManager;
    SceneManage sceneManager;
    public GameObject ParazLobbyPrefab;

    // Start is called before the first frame update
    void Start()
    {
        playerManager = GameObject.Find("GameManager").GetComponent<PlayerManager>();
        sceneManager = GameObject.Find("GameManager").GetComponent<SceneManage>();
        playerManager.OnNewPlayer += onNewPlayer;

        spawnAllParazLobby();


    }

    // Update is called once per frame
    void Update()
    {
        
    }



    void spawnAllParazLobby()
    {
        if (playerManager.getNumOfPlayers() == 0)
            return;
        GameObject.Find("UI").GetComponent<UImanager>().changeTextA("dontCare", false, true); //shutdown text       
        Dictionary<int,Player> playerList = playerManager.PlayersList;   
        foreach (var id in playerManager.PlayersList.Keys)
        {
            playerList[id].InstantiateNewParaz(ParazLobbyPrefab, new Vector3(0, 0, 0));
            if (playerList[id].paraz.GetComponent<AndroidMovement>().enabled)
                playerList[id].paraz.GetComponent<AndroidMovement>().setId(id);
            else
                playerList[id].paraz.GetComponent<PythonMovement>().setId(id);
        }
    }


    void onNewPlayer(Player player)
    {
        player.InstantiateNewParaz(ParazLobbyPrefab, new Vector3(0, 0, 0));
        player.paraz.GetComponent<AndroidMovement>().setId(player.id);
        if (playerManager.getNumOfPlayers() == 1)
            GameObject.Find("UI").GetComponent<UImanager>().changeTextA("care", false, true); //shutdown text 
    }
/*
    private void OnDisable()
    {
        playerManager.OnNewPlayer -= onNewPlayer;
    }*/
}

