using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerManager : MonoBehaviour
{
    messageQueue queue;
    public GameObject parazLobby; //prefab
    public GameObject parazRace; //prefab

    public Dictionary<int, Player> PlayersList;
    int numOfPlayers;
    Server server;

    Color[] colors;



    // Start is called before the first frame update
    void Start()
    {   
        queue = new messageQueue();
        PlayersList = new Dictionary<int, Player>();
        numOfPlayers = 0;
        server = GameObject.Find("Server").GetComponent<Server>();
        colors =new Color[3] { Color.yellow, Color.blue, Color.green };

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
                    NewPlayer(Int32.Parse(message.Substring(3)));          
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

    public void NewPlayer(int id)
    {
        numOfPlayers++;
        PlayersList.Add(id, new Player(id, getPlayerTcpQueue(), colors[id]));
        PlayersList[id].InstantiateNewParaz(parazLobby, new Vector3(0, 0, 0));
        PlayersList[id].paraz.GetComponent<AndroidMovement>().setId(id);
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


    public void onLobbySceneLoad()
    {
        foreach (var id in PlayersList.Keys)
        {
            PlayersList[id].InstantiateNewParaz(parazLobby, new Vector3(0, 0, 0));
            if(PlayersList[id].paraz.GetComponent<AndroidMovement>().enabled)
                 PlayersList[id].paraz.GetComponent<AndroidMovement>().setId(id);
            else
                PlayersList[id].paraz.GetComponent<PythonMovement>().setId(id);
        }
    }


    public void onRaceSceneLoad()
    {
        Vector3 startPos = GameObject.Find("SpawnPos").transform.position;
        foreach (var id in PlayersList.Keys)
        {
            PlayersList[id].InstantiateNewParaz(parazRace, startPos);
            if (PlayersList[id].paraz.GetComponent<AndroidRaceMovement>().enabled)
                PlayersList[id].paraz.GetComponent<AndroidRaceMovement>().setId(id);
            else
                PlayersList[id].paraz.GetComponent<pythonRaceMovement>().setId(id);

        }

    }

    public class Player
    {
        public int id;
        bool isUp;
        int score;
        public GameObject paraz; //prefab; 
        public messageQueue queue;
        Color color;

        public Player(int id, messageQueue queue, Color color)
        {
            this.queue = queue;
            this.id = id;
            this.color = color;
        }


        public void InstantiateNewParaz(GameObject paraz_prefab, Vector3 startPos)
        {
            Debug.Log("sp bitch");
            paraz = Instantiate(paraz_prefab, startPos, Quaternion.identity) as GameObject;
            implementColor();
        }

        private void implementColor()
        {
            paraz.transform.Find("arrow").GetComponent<SpriteRenderer>().color = color;
        }
    }


}
