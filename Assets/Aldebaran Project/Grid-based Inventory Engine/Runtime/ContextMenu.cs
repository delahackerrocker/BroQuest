using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContextMenu : MonoBehaviour
{
    public InventoryItem item;

    public Inventory inventory;

    private void Awake()
    {
        InventoryCommon.contextMenu = this;
    }

    private void Start()
    {
        // inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
        // player = GameObject.FindGameObjectWithTag("Player");
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(HideAfterTime());
        }
    }

    IEnumerator HideAfterTime()
    {
        yield return new WaitForSeconds(.1f);
        Hide(); 
    }

    public void ShowDetails()
    {
        inventory.SelectItem(item.M_item);
        Hide();
    }

    public void Drop()
    {
        inventory.DropItem(item);
        Hide();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
    public void Show(InventoryItem inventoryItem)
    {
        gameObject.SetActive(true);
        this.item = inventoryItem;
        transform.position = Input.mousePosition;
    }
}
