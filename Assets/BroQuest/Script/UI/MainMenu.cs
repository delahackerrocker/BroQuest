using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class MainMenu : MonoBehaviourPunCallbacks, ILobbyCallbacks
{
    // Screens
    [Header("UI Screens")]
    public GameObject mainScreen;
    public GameObject createRoomScreen;
    public GameObject lobby;

    // Main Screen
    [Header("Main")]
    public Button createRoomBTN;
    public Button findRoomBTN;
    public Button backBTN;

    // Create Room Screen
    [Header("Create Room")]
    public Button createBTN;

    // Lobby Screen
    [Header("Lobby")]
    public TextMeshProUGUI playerListTXT;
    public TextMeshProUGUI roomInfoTXT;
    public Button startGameBTN;

    void Start()
    {
        // Disable the menu buttons at the start
        createRoomBTN.interactable = false;
        findRoomBTN.interactable = false;
        startGameBTN.interactable = false;

        // Enable the cursor
        Cursor.lockState = CursorLockMode.None;

        if (PhotonNetwork.InRoom)
        {
            // Go to the Lobby

            // Make the game visible again
            PhotonNetwork.CurrentRoom.IsVisible = true;
            PhotonNetwork.CurrentRoom.IsOpen = true;
        }

        SetScreen(mainScreen);
    }

    void SetScreen(GameObject screen)
    {
        HideUI();
        screen.SetActive(true);
    }

    void HideUI()
    {
        mainScreen.SetActive(false);
        createRoomScreen.SetActive(false);
        lobby.SetActive(false);
    }

    public void OnBackBTN()
    {
        SetScreen(mainScreen);
    }

    public void OnPlayerNameValueChanged(TMP_InputField playerNameInput)
    {
        PhotonNetwork.NickName = playerNameInput.text;
        Debug.Log("You player name is " + PhotonNetwork.NickName);
    }

    public override void OnConnectedToMaster()
    {
        createRoomBTN.interactable = true;
        findRoomBTN.interactable = true;
    }

    public void OnCreateRoomBTN()
    {
        SetScreen(createRoomScreen);
    }
    public void OnFindRoomBTN()
    {
        JoinRandomRoom();
    }

    public void OnCreateBTN(TMP_InputField roomNameInput)
    {
        NetworkManager.instance.CreateRoom(roomNameInput.text);
    }

    // Lobby Screen
    public override void OnJoinedRoom()
    {
        SetScreen(lobby);

        // Update UI for everyone already in the room
        photonView.RPC("UpdateLobbyUI", RpcTarget.All);
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log(otherPlayer+" has left the room." );
        UpdateLobbyUI();
    }


    // RPC Function allows you to make calls to other players who are not this client
    [PunRPC]
    void UpdateLobbyUI()
    {
        startGameBTN.interactable = PhotonNetwork.IsMasterClient;

        playerListTXT.text = "";

        Debug.Log("You player name is " + PhotonNetwork.NickName);
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            playerListTXT.text += player.NickName + "\n";
        }

        roomInfoTXT.text = PhotonNetwork.CurrentRoom.Name;

        if (!PhotonNetwork.IsMasterClient)
        {
            NetworkManager.instance.photonView.RPC("ChangeScene", RpcTarget.All, "MultiUserBoard");
        }
    }

    public void OnStartGameBTN()
    {
        // Close the room
        PhotonNetwork.CurrentRoom.IsOpen = true;

        // Hide the room
        PhotonNetwork.CurrentRoom.IsVisible = true;

        // All players will start the game
        NetworkManager.instance.photonView.RPC("ChangeScene", RpcTarget.All, "MultiUserBoard");
    }

    public void OnLeaveLobbyBTN()
    {
        PhotonNetwork.LeaveRoom();
        SetScreen(mainScreen); 
    }

    // Lobby Browser Screen - TODO
    public void JoinRandomRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }
}
