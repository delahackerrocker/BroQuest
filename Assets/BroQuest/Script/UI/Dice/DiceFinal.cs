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

    void SetFinal(int final)
    {
        finalRead.text = ""+final;
    }
}