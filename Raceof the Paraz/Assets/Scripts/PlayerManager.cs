using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerManager : MonoBehaviour
{
    messageQueue queue;
    public GameObject parazLobby; //prefab
    public GameObject parazRace; //prefab

    public delegate void callback(Player player);
    public event callback OnNewPlayer;
    public event callback OnPlayerLeft;

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
        colors =new Color[4] { Color.yellow, Color.blue, Color.green, Color.red };

    }

    // Update is called once per frame
    void FixedUpdate()
    {


        if (!queue.Isempty())
        {
            string message;
            message = queue.Dequeue();
            Debug.Log(message);
            if (message.Contains("new"))   //message is new#id
                if (GetComponent<SceneManage>().getSceneName() == "Lobby")
                {
                    Player newPlayer = NewPlayer(Int32.Parse(message.Substring(3)));
                    OnNewPlayer(newPlayer);  //event

                }
        }
    }

    //void addToOnNewPlayerCallback(var )

    public Player NewPlayer(int clientHandlerId)
    {
        Player newPlayer;
        int id = getNewPlayerId();
        numOfPlayers++;
        Debug.Log(clientHandlerId);
        newPlayer = new Player(id, getNewPlayerUdpQueue(clientHandlerId), colors[id]);
        PlayersList.Add(id, newPlayer);     

        return newPlayer;
    }

    private int getNewPlayerId()
    {
        for(int i = 0; i<numOfPlayers; i++)
        {
            if (!PlayersList.ContainsKey(i))
                 return i;
        }
        return numOfPlayers;
    }

    public messageQueue getNewPlayerUdpQueue(int clientHandlerId)
    {
        return server.GetTcpClientHandler(clientHandlerId).udpHandler.queue;
    }

    public messageQueue getPlayerUdpQueue(int id)
    {
        return PlayersList[id].queue;
    }


    public messageQueue getPlayerManagerQueue()
    {
        return queue;
    }

    
    public int getNumOfPlayers()
    {
        return numOfPlayers;
    }
    
    /*
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
    */

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
        bool isActive = false;
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
            isActive = true;
            implementColor();
        }

        private void implementColor()
        {
            paraz.transform.Find("arrow").GetComponent<SpriteRenderer>().color = color;
        }
    }


}
