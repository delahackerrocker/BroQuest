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
    public static ZargonFeed instance;

    public TextMeshProUGUI characterName;

    public GameObject diceContainer;

    [SerializeField] List<GameObject> diceGroup = new List<GameObject>();

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        ZargonClear();
    }

    public void OnUpdateFeed(string[,] newRolls)
    {
        if (newRolls.Length > 0)
        {
            Debug.Log("newRolls.Length:"+newRolls.Length/2);
            int diceCount = newRolls.Length/2;
            for (int i = 0; i < diceCount; i++)
            {
                Debug.Log("newRolls[i, 0]:" + newRolls[i, 0]);
                Debug.Log("newRolls[i, 1]:" + newRolls[i, 1]);
                LocalDiceDisplay(PhotonNetwork.LocalPlayer.NickName, i, newRolls[i, 0], newRolls[i, 1]);
            }
        }
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void ZargonClear()
    {
        for (int i = 0; i < diceGroup.Count; i++)
        {
            diceGroup[i].GetComponent<DiceFinal>().SetNone();
        }
    }

    void LocalDiceDisplay(string playerName, int diceID, string newRollType, string newRollValue)
    {
        diceGroup[diceID].gameObject.SetActive(true);
        diceGroup[diceID].GetComponent<DiceFinal>().SetFinal(newRollType, newRollValue);
    }

        // copypasta for global roll history tab
        // rollHistory += string.Format("<b>{0}:</b> {1}\n", playerName, newRoll);
}