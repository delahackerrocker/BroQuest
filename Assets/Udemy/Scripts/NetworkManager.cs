using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public int numPlayers;

    public static NetworkManager instance;

    private void Awake()
    {
        if ((instance != null) && (instance != this))
        {
            gameObject.SetActive(false);
        } else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        // connect to master server
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("You joined the master server.");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("You joined the lobby:");
    }
    public override void OnJoinedRoom()
    {
        Debug.Log("You joined room "+PhotonNetwork.CurrentRoom.Name);
    }

    public void CreateRoom(string roomName)
    {
        RoomOptions options = new RoomOptions();
        options.MaxPlayers = (byte)numPlayers;

        PhotonNetwork.CreateRoom(roomName, options);
    }

    public void JoinRoom(string roomName)
    {
        PhotonNetwork.JoinRoom(roomName);

    }

    public void ChangeScene(string sceneName)
    { 
        PhotonNetwork.LoadLevel(sceneName);
    }
}
