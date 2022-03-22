using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class TooltipRaycaster : MonoBehaviour
{
    GraphicRaycaster m_Raycaster;
    PointerEventData m_PointerEventData;
    EventSystem m_EventSystem;

    void Start()
    {
        //Fetch the Raycaster from the GameObject (the Canvas)
        m_Raycaster = GetComponent<GraphicRaycaster>();
        //Fetch the Event System from the Scene
        m_EventSystem = GetComponent<EventSystem>();
    }

    void Update()
    {
        //Set up the new Pointer Event
        m_PointerEventData = new PointerEventData(m_EventSystem);
        //Set the Pointer Event Position to that of the mouse position
        m_PointerEventData.position = Input.mousePosition;

        //Create a list of Raycast Results
        List<RaycastResult> results = new List<RaycastResult>();

        //Raycast using the Graphics Raycaster and mouse position
        m_Raycaster.Raycast(m_PointerEventData, results);

        bool flag = false;
        foreach (RaycastResult result in results)
        {
            if (result.gameObject.GetComponent<InventoryItem>() != null)
            {
                InventoryCommon.highlitedItem = result.gameObject.GetComponent<InventoryItem>().M_item;
                flag = true;
            }
        }

        if (!flag)
        {
            InventoryCommon.highlitedItem = null;
        }
    }
}
