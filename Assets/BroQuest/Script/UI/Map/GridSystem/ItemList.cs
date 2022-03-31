using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemListObject
{
    public Item[] items;
}

public class ItemList
{
    static ItemListObject itemListObject;
    public static List<Item> items; // lists all available items in the game
    public static Dictionary<string, int> types; // lists item types and auto sort priorities

    public static void GenerateList(TextAsset json)
    {
        items = new List<Item>();
        itemListObject = JsonUtility.FromJson<ItemListObject>(json.text);
        foreach (Item item in itemListObject.items)
        {
            items.Add(item);
        }

        /* For now types are being separated into their own files because they will get their own map layers
         * 
        types = new Dictionary<string, int>();

        // This is where you can add priorities to the auto-sorting algorithm. Lower numbers are sorted first.
        types.Add("Weapon", 0);
        types.Add("Potion", 1);
        types.Add("Equipment", 2);
        *
        */
    }
}
