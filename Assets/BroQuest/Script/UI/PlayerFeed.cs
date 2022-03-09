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
    public static PlayerFeed instance;

    public TextMeshProUGUI characterName;

    public string rollHistory = "";

    [SerializeField] List<GameObject> diceGroup = new List<GameObject>();
    [SerializeField] GameObject D4;
    [SerializeField] GameObject D6;
    [SerializeField] GameObject D8;
    [SerializeField] GameObject D10;
    [SerializeField] GameObject D12;
    [SerializeField] GameObject D20;

    private void Awake()
    {
        instance = this;
    }
    void Update()
    {
        ;
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
        characterName.text = playerName;
        rollHistory += string.Format("<b>{0}:</b> {1}\n", playerName, message);
    }
}