using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class MapItemList : MonoBehaviour
{
    public static MapItemList Instance { get; private set; }

    public bool isSetup = false;

    public GridLayoutGroup gridLayoutGroup;

    private List<Item> items;

    public GameObject mapItemTemplate;
    public GameObject[] mapItems;

    private void Start()
    {
        items = new List<Item>();
    }

    // When the item list is ready we will make a copy of it
    private void Update()
    {
        if (isSetup)
        {
            // Do Nothing
        } else
        {
            if (ItemList.items != null)
            {
                mapItems = new GameObject[ItemList.items.Count];
                int count = 0;
                foreach (Item item in ItemList.items)
                {
                    items.Add(item);
                    mapItems[count] = Instantiate(mapItemTemplate, new Vector3(0, 0, 0), Quaternion.identity);
                    mapItems[count].transform.SetParent(gridLayoutGroup.transform);
                    mapItems[count].transform.localScale = Vector3.one;
                    mapItems[count].GetComponent<MapItem>().Setup(item.id_i, item.spritePath, item.name);
                    count++;
                }

                isSetup = true;
            }
        }
    }
}