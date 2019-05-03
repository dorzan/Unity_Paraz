﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

public class Server : MonoBehaviour
{
    PlayerManager playerManager;
    public static int numOfConnetctions;
    #region private members 	
    /// <summary> 	
    /// TCPListener to listen for incomming TCP connection 	
    /// requests. 	
    /// </summary> 	
    private TcpListener tcpListener;
    /// <summary> 
    /// Background thread for TcpServer workload. 	
    /// </summary> 	
    private Thread tcpListenerThread;
    private static List<TcpClientHandler> TcpClientHandlerList;
    /// <summary> 	
    /// Create handle to connected tcp client. 	
    /// </summary> 	
    //private List<TcpClient> connectedTcpClientList;
    // private TcpClient connectedTcpClient;
    #endregion

    // Use this for initialization
    void Start()
    {
        DontDestroyOnLoad(this);
        numOfConnetctions = 0;
        TcpClientHandlerList = new List<TcpClientHandler>();
       
        // Start TcpServer background thread 	
        tcpListenerThread = new Thread(new ThreadStart(ListenForIncommingRequests));
        tcpListenerThread.IsBackground = true;
        tcpListenerThread.Start();
        playerManager = GameObject.Find("GameManager").GetComponent<PlayerManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnLevelWasLoaded(int level)
    {

    }
   
    public TcpClientHandler GetClientHandler(int id)
    {
        try
        {
            return TcpClientHandlerList[id];
        }
        catch(ArgumentOutOfRangeException asd){
            return null;
                }
       
    }

    /// <summary> 	
    /// Runs in background TcpServerThread; Handles incomming TcpClient requests 	
    /// </summary> 	
    /// 
    private void ListenForIncommingRequests()
    {
        try
        {
            // Create listener on localhost port 8052. 			
            tcpListener = new TcpListener(IPAddress.Any, 7808);
            tcpListener.Start();
            Debug.Log("Server is listening");
            while (true)
            {
                TcpClient connectedTcpClient = tcpListener.AcceptTcpClient();
                {
                    Debug.Log("connection num" +(numOfConnetctions) +" established ");
                    TcpClientHandlerList.Add(new TcpClientHandler(connectedTcpClient, numOfConnetctions));
                    playerManager.getPlayerManagerQueue().Insert("new" + numOfConnetctions.ToString()+'$');
                    numOfConnetctions++;

                    // Get a stream object for reading
                }
            }
        }
        catch (SocketException socketException)
        {
            Debug.Log("SocketException " + socketException.ToString());
        }
    }




    public class TcpClientHandler
    {
        TcpClient clientSocket;
        int id;
        public messageQueue queue;
        private Thread tcpClientThread;

        public TcpClientHandler(TcpClient clientSocket, int id)
        {
            queue = new messageQueue();
            this.clientSocket = clientSocket;
            this.id = id;
            tcpClientThread = new Thread(new ThreadStart(ListenToClient));
            tcpClientThread.IsBackground = true;
            tcpClientThread.Start();

        }

   

        public void ListenToClient()
        {
            using (NetworkStream stream = clientSocket.GetStream())
            {
                Byte[] bytes = new Byte[1024];
                int length;
                // Read incomming stream into byte arrary. 						
                while ((length = stream.Read(bytes, 0, bytes.Length)) != 0)
                {
                    string clientMessage;
                    var incommingData = new byte[length];
                    Array.Copy(bytes, 0, incommingData, 0, length);
                    // Convert byte array to string message. 							
                    clientMessage = Encoding.ASCII.GetString(incommingData);
                    queue.Insert(clientMessage);
                   // Debug.Log("client  " + id +"message received as: " + clientMessage);
                }
            }
        }
    }
}





    /// <summary> 	
    /// Send message to client using socket connection. 	
    /// </summary> 	
    /// 
    /*
    private void SendMessage()
    {
        if (connectedTcpClient == null)
        {
            return;
        }

        try
        {
            // Get a stream object for writing. 			
            NetworkStream stream = connectedTcpClient.GetStream();
            if (stream.CanWrite)
            {
                string serverMessage = "This is a message from your server.";
                // Convert string message to byte array.                 
                byte[] serverMessageAsByteArray = Encoding.ASCII.GetBytes(serverMessage);
                // Write byte array to socketConnection stream.               
                stream.Write(serverMessageAsByteArray, 0, serverMessageAsByteArray.Length);
                Debug.Log("Server sent his message - should be received by client");
            }
        }
        catch (SocketException socketException)
        {
            Debug.Log("Socket exception: " + socketException);
        }
    }
}*/