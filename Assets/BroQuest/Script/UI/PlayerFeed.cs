using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class PlayerFeed : MonoBehaviourPun
{
    public TextMeshProUGUI feed;

    public static PlayerFeed instance;

    public string rollHistory = "";

    private void Awake()
    {
        instance = this;
    }
    void Update()
    {
        feed.text = rollHistory;
    }

    public void OnUpdateFeed(string newRoll)
    {
        if (newRoll.Length > 0)
        {
            photonView.RPC("ThreadPlayer", RpcTarget.All, PhotonNetwork.LocalPlayer.NickName, newRoll);
        }
        EventSystem.current.SetSelectedGameObject(null);
    }

    [PunRPC]
    void ThreadPlayer(string playerName, string message)
    {
        rollHistory += string.Format("<b>{0}:</b> {1}\n", playerName, message);
    }
}
