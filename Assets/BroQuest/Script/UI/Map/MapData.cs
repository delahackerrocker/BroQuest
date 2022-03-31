using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapData : MonoBehaviour
{
    public TextAsset items;

    void Start()
    {
        ItemList.GenerateList(items);
    }

    void Update()
    {
        
    }
}
