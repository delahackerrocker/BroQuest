using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class PlayerList : MonoBehaviourPunCallbacks, ILobbyCallbacks
{
    public TextMeshProUGUI playerListTMP;

    private List<RoomInfo> roomList = new List<RoomInfo>();

    void Start()
    {
        UpdatePlayerList();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("You joined the room.");
        photonView.RPC("UpdatePlayerList", RpcTarget.All);
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log(otherPlayer + " has left the room.");
        UpdatePlayerList();
    }


    // RPC Function allows you to make calls to other players who are not this client
    [PunRPC]
    void UpdatePlayerList()
    {
        playerListTMP.text = "";

        Debug.Log("You player name is " + PhotonNetwork.NickName);
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            playerListTMP.text += player.NickName + "\n";
        }
    }
}
