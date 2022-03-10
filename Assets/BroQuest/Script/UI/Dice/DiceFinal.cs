using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class DiceFinal : MonoBehaviour
{
    public TextMeshProUGUI finalRead;
    public Image[] diceImages;

    public void SetFinal(string diceType, string finalRead)
    {
        Debug.Log("SetFinal:" + finalRead);
        this.finalRead.text = finalRead;
        
        foreach (Image image in diceImages)
        {
            image.gameObject.SetActive(false);
        }

        if (diceType == "4")
        {
            diceImages[0].gameObject.SetActive(true);
        }
        else if (diceType == "6")
        {
            diceImages[1].gameObject.SetActive(true);
        }
        else if (diceType == "8")
        {
            diceImages[2].gameObject.SetActive(true);
        }
        else if (diceType == "10")
        {
            diceImages[3].gameObject.SetActive(true);
        }
        else if (diceType == "12")
        {
            diceImages[4].gameObject.SetActive(true);
        }
        else // "20"
        {
            diceImages[5].gameObject.SetActive(true);
        }
    }

    public void SetNone()
    {
        this.finalRead.text = "";

        foreach (Image image in diceImages)
        {
            image.gameObject.SetActive(false);
        }
    }
}