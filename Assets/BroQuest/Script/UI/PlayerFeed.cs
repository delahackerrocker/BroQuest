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

    public GameObject diceContainer;

    [SerializeField] List<GameObject> diceGroup = new List<GameObject>();

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        ThreadPlayerClear(PhotonNetwork.NickName);
    }

    public void OnUpdateFeed(string[,] newRolls)
    {
        if (newRolls.Length > 0)
        {
            Debug.Log("newRolls.Length:" + newRolls.Length / 2);
            int diceCount = newRolls.Length / 2;
            for (int i = 0; i < diceCount; i++)
            {
                Debug.Log("newRolls[i, 0]:" + newRolls[i, 0]);
                Debug.Log("newRolls[i, 1]:" + newRolls[i, 1]);

                photonView.RPC("ThreadPlayerDiceDisplay_" + i, RpcTarget.All, newRolls[i, 0], newRolls[i, 1]);

                //UpdateDiceRollLog(PhotonNetwork.LocalPlayer.NickName, i, newRolls[i, 0], newRolls[i, 1]);
            }
        }
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void PlayerClear()
    {
        photonView.RPC("ThreadPlayerClear", RpcTarget.All, PhotonNetwork.NickName);
    }
    [PunRPC]
    void ThreadPlayerClear(string characterName)
    {
        this.characterName.text = characterName;
        for (int i = 0; i < diceGroup.Count; i++)
        {
            diceGroup[i].GetComponent<DiceFinal>().SetNone();
        }
    }

    [PunRPC]
    void ThreadPlayerDiceDisplay_0(string rt = null, string rv = null)
    {
        diceGroup[0].gameObject.SetActive(true);
        diceGroup[0].GetComponent<DiceFinal>().SetFinal(rt, rv);
    }
    [PunRPC]
    void ThreadPlayerDiceDisplay_1(string rt = null, string rv = null)
    {
        diceGroup[1].gameObject.SetActive(true);
        diceGroup[1].GetComponent<DiceFinal>().SetFinal(rt, rv);
    }
    [PunRPC]
    void ThreadPlayerDiceDisplay_2(string rt = null, string rv = null)
    {
        diceGroup[2].gameObject.SetActive(true);
        diceGroup[2].GetComponent<DiceFinal>().SetFinal(rt, rv);
    }
    [PunRPC]
    void ThreadPlayerDiceDisplay_3(string rt = null, string rv = null)
    {
        diceGroup[3].gameObject.SetActive(true);
        diceGroup[3].GetComponent<DiceFinal>().SetFinal(rt, rv);
    }
    [PunRPC]
    void ThreadPlayerDiceDisplay_4(string rt = null, string rv = null)
    {
        diceGroup[4].gameObject.SetActive(true);
        diceGroup[4].GetComponent<DiceFinal>().SetFinal(rt, rv);
    }
    [PunRPC]
    void ThreadPlayerDiceDisplay_5(string rt = null, string rv = null)
    {
        diceGroup[5].gameObject.SetActive(true);
        diceGroup[5].GetComponent<DiceFinal>().SetFinal(rt, rv);
    }
    [PunRPC]
    void ThreadPlayerDiceDisplay_6(string rt = null, string rv = null)
    {
        diceGroup[6].gameObject.SetActive(true);
        diceGroup[6].GetComponent<DiceFinal>().SetFinal(rt, rv);
    }
    [PunRPC]
    void ThreadPlayerDiceDisplay_7(string rt = null, string rv = null)
    {
        diceGroup[7].gameObject.SetActive(true);
        diceGroup[7].GetComponent<DiceFinal>().SetFinal(rt, rv);
    }
}