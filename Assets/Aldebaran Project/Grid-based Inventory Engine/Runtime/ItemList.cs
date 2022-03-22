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

    public static void GenerateList()
    {
        string json = "";
#if UNITY_EDITOR
        json = Resources.Load<TextAsset>("Json/items").text;
#else
        string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        path = System.IO.Directory.GetParent(path).FullName;
        path = System.IO.Path.Combine(
            System.IO.Path.GetDirectoryName(path), 
            "Data\\Json\\items.json" // the path in your game build folder to the JSON file - you can change this if you want
            );
        json = System.IO.File.ReadAllText(path);
        Debug.Log(json);
#endif

        items = new List<Item>();
        itemListObject = JsonUtility.FromJson<ItemListObject>(json);
        foreach (Item item in itemListObject.items)
        {
            items.Add(item);
        }

        types = new Dictionary<string, int>();

        // This is where you can add priorities to the auto-sorting algorithm. Lower numbers are sorted first.
        types.Add("Weapon", 0);
        types.Add("Potion", 1);
        types.Add("Equipment", 2);
    }
}
