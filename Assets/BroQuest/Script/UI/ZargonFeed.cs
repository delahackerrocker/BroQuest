using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class ZargonFeed : MonoBehaviourPun
{
    public TextMeshProUGUI feed;

    public static ZargonFeed instance;

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
            photonView.RPC("ThreadZargon", RpcTarget.All, PhotonNetwork.LocalPlayer.NickName, newRoll);
        }
        EventSystem.current.SetSelectedGameObject(null);
    }

    [PunRPC]
    void ThreadZargon(string playerName, string message)
    {
        rollHistory += string.Format("<b>{0}:</b> {1}\n", playerName, message);
    }
}
