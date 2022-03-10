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
    void Update()
    {
        ;
    }

}