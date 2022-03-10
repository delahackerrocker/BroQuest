using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class RollManager : MonoBehaviourPunCallbacks, ILobbyCallbacks
{
    public Camera diceCamera;

    [SerializeField] List<GameObject> selectionDiceGroup = new List<GameObject>();

    [SerializeField] GameObject zargonRollManager;
    [SerializeField] GameObject playerRollManager;

    [SerializeField] List<GameObject> zargonDiceGroup = new List<GameObject>();
    [SerializeField] RollJump zargonRollJump;
    [SerializeField] RollDrop zargonRollDrop;

    [SerializeField] GameObject zargonD4;
    [SerializeField] GameObject zargonD6;
    [SerializeField] GameObject zargonD8;
    [SerializeField] GameObject zargonD10;
    [SerializeField] GameObject zargonD12;
    [SerializeField] GameObject zargonD20;

    [SerializeField] GameObject zargonDiceSpawnPoint;

    [SerializeField] List<GameObject> playerDiceGroup = new List<GameObject>();
    [SerializeField] RollJump playerRollJump;
    [SerializeField] RollDrop playerRollDrop;

    [SerializeField] GameObject playerD4;
    [SerializeField] GameObject playerD6;
    [SerializeField] GameObject playerD8;
    [SerializeField] GameObject playerD10;
    [SerializeField] GameObject playerD12;
    [SerializeField] GameObject playerD20;

    [SerializeField] GameObject playerDiceSpawnPoint;


    void Start()
    {
        diceCamera = GameObject.FindGameObjectWithTag("Dice_Camera").GetComponent<Camera>() as Camera;
        UpdateDiceSets();
    }

    void UpdateDiceSets()
    {
        zargonRollJump.SetDiceGroup(zargonDiceGroup);
        zargonRollDrop.SetDiceGroup(zargonDiceGroup);

        playerRollJump.SetDiceGroup(playerDiceGroup);
        playerRollDrop.SetDiceGroup(playerDiceGroup);
    }

    // See Order of Execution for Event Functions for information on FixedUpdate() and Update() related to physics queries
    void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Bit shift the index of the layer (8) to get a bit mask
            int layerMask = 7;

            // This would cast rays only against colliders in layer 8.
            // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
            layerMask = ~layerMask;

            RaycastHit hit;
            Ray ray = diceCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, 100))
            {
                //print("Hit something!");
            }

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            {
                //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);

                
                if (zargonDiceGroup.Contains(hit.collider.gameObject))
                {
                    Debug.Log("Did Hit :: " + hit.collider.name);   
                    zargonDiceGroup.Remove(hit.collider.gameObject);
                    Destroy(hit.collider.gameObject);
                }
                if (playerDiceGroup.Contains(hit.collider.gameObject))
                {
                    Debug.Log("Did Hit :: " + hit.collider.name);
                    playerDiceGroup.Remove(hit.collider.gameObject);
                    Destroy(hit.collider.gameObject);
                }
                

                // check selection dice
                if (selectionDiceGroup.Contains(hit.collider.gameObject))
                {
                    GameObject newDice = new GameObject();

                    //Debug.Log("Did Hit Selection :: " + hit.collider.name);

                    if (PhotonNetwork.IsMasterClient)
                    {
                        if (hit.collider.name == "D4")
                        {
                            newDice = Instantiate(zargonD4, zargonDiceSpawnPoint.transform.position, transform.rotation);
                        }
                        else if (hit.collider.name == "D6")
                        {
                            newDice = Instantiate(zargonD6, zargonDiceSpawnPoint.transform.position, transform.rotation);
                        }
                        else if (hit.collider.name == "D8")
                        {
                            newDice = Instantiate(zargonD8, zargonDiceSpawnPoint.transform.position, transform.rotation);
                        }
                        else if (hit.collider.name == "D10")
                        {
                            newDice = Instantiate(zargonD10, zargonDiceSpawnPoint.transform.position, transform.rotation);
                        }
                        else if (hit.collider.name == "D12")
                        {
                            newDice = Instantiate(zargonD12, zargonDiceSpawnPoint.transform.position, transform.rotation);
                        }
                        else if (hit.collider.name == "D20")
                        {
                            newDice = Instantiate(zargonD20, zargonDiceSpawnPoint.transform.position, transform.rotation);
                        }
                        zargonDiceGroup.Add(newDice);
                        UpdateDiceSets();
                    } else {
                        if (hit.collider.name == "D4")
                        {
                            newDice = Instantiate(playerD4, playerDiceSpawnPoint.transform.position, transform.rotation);
                        }
                        else if (hit.collider.name == "D6")
                        {
                            newDice = Instantiate(playerD6, playerDiceSpawnPoint.transform.position, transform.rotation);
                        }
                        else if (hit.collider.name == "D8")
                        {
                            newDice = Instantiate(playerD8, playerDiceSpawnPoint.transform.position, transform.rotation);
                        }
                        else if (hit.collider.name == "D10")
                        {
                            newDice = Instantiate(playerD10, playerDiceSpawnPoint.transform.position, transform.rotation);
                        }
                        else if (hit.collider.name == "D12")
                        {
                            newDice = Instantiate(playerD12, playerDiceSpawnPoint.transform.position, transform.rotation);
                        }
                        else if (hit.collider.name == "D20")
                        {
                            newDice = Instantiate(playerD20, playerDiceSpawnPoint.transform.position, transform.rotation);
                        }
                        playerDiceGroup.Add(newDice);
                        UpdateDiceSets();
                    }

                }
            }
            else
            {
                //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
                //Debug.Log("Did not Hit");
            }
        }
    }
}