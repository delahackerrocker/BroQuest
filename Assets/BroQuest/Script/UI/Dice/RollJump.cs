using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class RollJump : MonoBehaviourPun
{
    // This list shows what dice are going to be and can be edited via code if you want or in the inspector. 
    [SerializeField] List<GameObject> diceGroup = new List<GameObject>();
    //Set what button is pressed to make the dice jump.
    [SerializeField] KeyCode buttonToJump = KeyCode.Space;
    [SerializeField] float forceAmount = 400f;

    private Vector3[] allDirections = { Vector3.left, Vector3.right, Vector3.forward, Vector3.back };

    private bool isUnlocked = true;

    public void SetDiceGroup(List<GameObject>  newDiceGroup)
    {
        diceGroup = newDiceGroup;
    }

    void Update()
    {
        JumpBehavior();
    }
    void JumpBehavior()
    {
        Rigidbody rb;
        if ((Input.GetKeyDown(buttonToJump)) && isUnlocked )
        {
            isUnlocked = false;

            for (int i = 0; i < diceGroup.Count; i++)
            {
                diceGroup[i].transform.rotation = Random.rotation;
                rb = diceGroup[i].GetComponent<Rigidbody>();
                rb.AddForce(Vector3.up * forceAmount); 
                rb.AddForce(allDirections[Random.Range(0, allDirections.Length)] * forceAmount);
                rb.AddTorque(new Vector3(Random.value * forceAmount, Random.value * forceAmount, Random.value * forceAmount));
            }

            if (PhotonNetwork.IsMasterClient)
            {
                Invoke("UpdateZargonFeed", 5f);
            } else
            {
                Invoke("UpdatePlayerFeed", 5f);
            }
        }
    }

    void UpdateZargonFeed()
    {
        string newRoll = "";

        for (int i = 0; i < diceGroup.Count; i++)
        {
            newRoll += "D?:" + diceGroup[i].GetComponent<DiceHighlight>().finalDiceRead+", ";
        }

        ZargonFeed.instance.OnUpdateFeed(newRoll);

        isUnlocked = true;
    }

    void UpdatePlayerFeed()
    {
        string newRoll = "";

        for (int i = 0; i < diceGroup.Count; i++)
        {
            newRoll += "D?:" + diceGroup[i].GetComponent<DiceHighlight>().finalDiceRead + ", ";
        }

        PlayerFeed.instance.OnUpdateFeed(newRoll);

        isUnlocked = true;
    }
}
   
