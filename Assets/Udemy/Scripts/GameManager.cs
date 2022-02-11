using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.Linq;

public class GameManager : MonoBehaviour
{
    [Header("Players")]
    public string PlayerPrefabPath;

    public Transform[] spawnPoints;
    public float respawnTime;

    private int playersInGame;

    // instance
    public static GameManager instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        NetworkManager.instance.photonView.RPC("ImInGame", RpcTarget.All);
    }

    [PunRPC]
    void ImInGame()
    {
        playersInGame++;
        if (playersInGame == PhotonNetwork.PlayerList.Length)
        {
            SpawnPlayer();
        }
    }

    void SpawnPlayer()
    {
        GameObject playerObj = PhotonNetwork.Instantiate(PlayerPrefabPath, 
            spawnPoints[Random.Range(0,spawnPoints.Length)].position, Quaternion.identity);

        // Initialize Player

    }
}