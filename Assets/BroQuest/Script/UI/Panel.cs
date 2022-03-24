using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class Panel : MonoBehaviour
{
    public Vector3 closePosition;
    public Vector3 openPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool panelIsOpen = true;
    public void TogglePanel()
    {
        if (panelIsOpen)
        {
            ClosePanel();
        }
        else
        {
            OpenPanel();
        }
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void OpenPanel()
    {
        panelIsOpen = true;
        this.transform.localPosition = openPosition;
    }
    public void ClosePanel()
    {
        panelIsOpen = false;
        this.transform.localPosition = closePosition;
    }
}
