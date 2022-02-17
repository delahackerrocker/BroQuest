using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

public class PlayerInfo : MonoBehaviourPun
{
    public TextMeshProUGUI nameTMP;
    public Image healthBar;
    private float maxValue;

    public void Initialize(string name, float maxValue)
    {
        nameTMP.text = name;
        this.maxValue = maxValue;
        healthBar.fillAmount = 1;
    }

    [PunRPC]
   void UpdateHealthBar(float value)
    {
        healthBar.fillAmount = value/maxValue;
    }
}
