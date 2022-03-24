using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class MapItem : MonoBehaviour
{
    public int ID;
    public Image image;
    public TextMeshProUGUI textMeshProUGUI;

    public void Setup(int id, string spritePath, string text)
    {
        this.ID = id;
        Texture2D texture = Resources.Load<Texture2D>(spritePath);
        Sprite sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);
        image.sprite = sprite;
        textMeshProUGUI.text = text;
    }

    public void AddItemToMap()
    {
        Debug.Log("AddItemToMap");
        InventoryItem inventoryItem = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>().AddItem(this.ID);
    }
}