using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerManager : MonoBehaviour
{
    messageQueue queue;
    public GameObject[] paraz; //prefab
    public GameObject[] parazRace; //prefab

    public List<Player> PlayersList;
    int numOfPlayers;
    Server server;


    // Start is called before the first frame update
    void Start()
    {   
        queue = new messageQueue();
        PlayersList = new List<Player>();
        numOfPlayers = 0;
        server = GameObject.Find("Server").GetComponent<Server>();

    }

    // Update is called once per frame
    void Update()
    {
        if (!queue.Isempty())
        {

            string message;
            message = queue.Dequeue();
            Debug.Log(message);
            if (message.Contains("new"))   //message is new#id
               if (GetComponent<SceneManage>().getSceneName() == "Lobby")
                    NewParaz(Int32.Parse(message.Substring(3)));          
        }
    }

    public messageQueue getPlayerTcpQueue()
    {
        return server.GetClientHandler(numOfPlayers-1).queue;
    }

    public messageQueue getPlayerTcpQueue(int id)
    {
        for(int i = 0; i < numOfPlayers; i ++)
            if (PlayersList[i].id == id)
                return PlayersList[i].queue;
        return null;
    }

    public void NewParaz(int id)
    {
        numOfPlayers++;
        PlayersList.Add(new Player(id, paraz[numOfPlayers-1], getPlayerTcpQueue()));
        PlayersList[PlayersList.Count - 1].paraz.GetComponent<AndroidMovement>().setId(id);
        // AudioManager.Instace.playSFX("boom");
        if (numOfPlayers == 1)
            GameObject.Find("UI").GetComponent<UImanager>().changeTextA("care", false, true); //shutdown text          
    }

    public messageQueue getPlayerManagerQueue()
    {
        return queue;
    }

    public int getNumOfPlayers()
    {
        return numOfPlayers;
    }


    public void loadLobby()
    {
        Debug.Log("SADFSADFSDF");
          for (int i=0; i < numOfPlayers; i++)
        {
            PlayersList[i].paraz = Instantiate(paraz[i], new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            PlayersList[i].paraz.GetComponent<AndroidMovement>().setId(PlayersList[i].id);

        }
    }

    public void onRaceSceneLoad()
    {
        Vector3 startPos = GameObject.Find("SpawnPos").transform.position;
        for (int i=0; i < numOfPlayers; i++)
        {
            PlayersList[i].paraz = Instantiate(parazRace[i], startPos , Quaternion.identity) as GameObject;
            Debug.Log("id is " + PlayersList[i].id);
            PlayersList[i].paraz.GetComponent<AndroidRaceMovement>().setId(PlayersList[i].id);

        }

    }



    public class Player
    {
        public int id;
        bool isUp;
        int score;
        public GameObject paraz; //prefab; 
        public messageQueue queue;

        public Player(int id, GameObject paraz_prefab, messageQueue queue)
        {
            this.queue = queue;
            this.id = id;
            this.paraz = Instantiate(paraz_prefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;        


        }
    }


}
