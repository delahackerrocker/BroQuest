using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using TMPro;

public class Inventory : MonoBehaviour
{
    [Header("CUSTOMIZATION")]
    [SerializeField, Tooltip("Number of squares in x axis.")]
    int xSquares = 1;
    [SerializeField, Tooltip("Number of squares in y axis.")]
    int ySquares = 1;
    [Tooltip("Square size.")]
    public float squareSize = 25;
    [Tooltip("Padding size around squares grid.")]
    public float padding = 15;

    [Space(20)]
    [Header("References (no need to modify these manually, add your items through AddItem())")]
    [Tooltip("Items currently inside the inventory.")]
    public List<GameObject> items;
    [HideInInspector] public List<GameObject> representableItems;
    [Tooltip("Grid square prefab.")]
    public GameObject square;
    public TextMeshProUGUI statsText;
    public GameObject player;
    public GameObject itemPrefab;


    Item selectedItem;
    InventoryItem grabbedItem;


    public int XSquares
    {
        get { return this.xSquares; }
        set {
            xSquares = value;
        }
    }
    public int YSquares
    {
        get { return this.ySquares; }
        set {
            ySquares = value;
        }
    }
    public float SquareSize
    {
        get { return this.squareSize; }
        set {
            squareSize = value;
        }
    }
    public float Padding
    {
        get { return this.padding; }
        set {
            padding = value;
        }
    }

    public InventoryItem GrabbedItem
    {
        get { return this.grabbedItem; }
        set
        {
            this.grabbedItem = value;
        }
    }

    List<GameObject> squares = new List<GameObject>();

    private void Start()
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player");

        UpdateInventory();
    }

    private void OnEnable()
    {
        SelectItem(null);
    }

    /// <summary>
    /// Deletes, creates and updates grid squares.
    /// </summary>
    public void UpdateInventory()
    {
        Debug.Log("Inventory::UpdateInventory");
        RectTransform myRect = GetComponent<RectTransform>();
        myRect.sizeDelta = new Vector2(xSquares * squareSize + padding, ySquares * squareSize + padding);

        if (squares.Count > 0)
        {
            foreach (GameObject square in squares)
            {
                Destroy(square);
            }
        }
        squares.Clear();
        for (int j = 0; j < ySquares; ++j)
        {
            for (int i = 0; i < xSquares; ++i)
            {
                GameObject inst = Instantiate(square, transform);
                squares.Add(inst);

                RectTransform rect = square.GetComponent<RectTransform>();
                rect.sizeDelta = new Vector2(squareSize, squareSize);

                rect.anchoredPosition = new Vector2(
                    i * squareSize + (padding / 2.0f),
                    -(j * squareSize + (padding / 2.0f))
                    );

                inst.name = "square" + "_" + i + "x" + "_" + j + "y";

            }
        }

        RectTransform firstSquareRect = squares[0].GetComponent<RectTransform>();
        firstSquareRect.sizeDelta = new Vector2(squareSize, squareSize);
        firstSquareRect.anchoredPosition = new Vector2(
                (xSquares - 1) * squareSize + (padding / 2.0f),
                -((ySquares - 1) * squareSize + (padding / 2.0f))
                );

        // change hierarchy order so item appears on top of grid
        foreach (GameObject item in items)
        {
            Transform itemHighlight = item.GetComponent<InventoryItem>().itemHighlight.transform;
            item.transform.SetParent(transform.parent, false);
            itemHighlight.SetParent(transform.parent, false);

            itemHighlight.SetParent(transform, false);
            item.transform.SetParent(transform, false);

            item.GetComponent<InventoryItem>().UpdateGridPositionAndSize();
        }
    }

    public void SelectItem(Item item)
    {
        Debug.Log("Inventory::SelectItem");
        this.selectedItem = item;
        UpdateStatsText();
    }


    void UpdateStatsText()
    {
        Debug.Log("Inventory::UpdateStatsText");
        if (selectedItem == null)
        {
            statsText.SetText("");
        }
        else
        {
            statsText.SetText("<alpha=#aa>Name: <alpha=#ff>" + selectedItem.name + "\n" +
                "<alpha=#aa>Weight: <alpha=#ff>{0}\n" +
                "<alpha=#aa>Type:  <alpha=#ff>" + selectedItem.type+"\n" +
                "<alpha=#aa>———————————————\n" + selectedItem.description,
                selectedItem.weight);
        }
    }

    /// <summary>
    /// Checks if a given position is inside inventory bounds.
    /// </summary>
    /// <param name="positionx"></param>
    /// <param name="positiony"></param>
    /// <returns></returns>
    public bool CheckInsideBounds(int positionx, int positiony, int width = 1, int height = 1)
    {
        Debug.Log("Inventory::CheckInsideBounds");
        Vector2Int topleft = new Vector2Int(positionx, positiony);
        Vector2Int bottomRight = new Vector2Int(positionx + width, positiony + height);
        bool topLeftInBounds = topleft.x >= 0 && topleft.y >= 0;
        bool bottomRightInBounds = bottomRight.x < xSquares + 1 && bottomRight.y < ySquares + 1;
        return topLeftInBounds && bottomRightInBounds;
    }

    public bool CheckCollisions(GameObject checkingItem, int checkPositionx, int checkPositiony, int width, int height)
    {
        Debug.Log("Inventory::CheckCollisions");
        foreach (GameObject item in items)
        {
            if (item != checkingItem)
            {
                Vector2Int myMin = new Vector2Int(checkPositionx, checkPositiony);
                Vector2Int myMax = new Vector2Int(checkPositionx + width, checkPositiony + height);
                InventoryItem otherInventoryItem = item.GetComponent<InventoryItem>();
                Vector2Int otherMin = new Vector2Int(otherInventoryItem.positionx, otherInventoryItem.positiony);
                Vector2Int otherMax = new Vector2Int(otherInventoryItem.positionx + otherInventoryItem.width, otherInventoryItem.positiony + otherInventoryItem.height);
                //print("otherMin " + otherMin + "    otherMax: " + otherMax);
                if (myMin.x < otherMax.x &&
                    myMax.x > otherMin.x &&
                    myMin.y < otherMax.y &&
                    myMax.y > otherMin.y)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public void RemoveItem(InventoryItem item)
    {
        Debug.Log("Inventory::RemoveItem");
        if (items.Contains(item.gameObject))
        {
            items.Remove(item.gameObject);
            Destroy(item.gameObject);
        }
    }

    public void DropItem(InventoryItem item)
    {
        Debug.Log("Inventory::DropItem");
        if (item.representedItem)
        {
            Vector2 aroundPlayerVector = new Vector2(UnityEngine.Random.Range(0.01f, 1.0f), UnityEngine.Random.Range(0.01f, 1.0f)).normalized;
            float spawnRadius = 0.5f;
            Vector3 spawnPosition = new Vector3(
                player.transform.position.x + aroundPlayerVector.x * spawnRadius,
                player.transform.position.y + 0.5f,
                player.transform.position.z + aroundPlayerVector.y * spawnRadius);
            GameObject inst = Instantiate(item.representedItem, spawnPosition, Quaternion.identity);

            RemoveItem(item);
            SelectItem(null);
        }
    }

    public bool GrabbingItem()
    {
        Debug.Log("Inventory::GrabbingItem");
        return grabbedItem != null;
    }

    /// <summary>
    /// Searches for empty spot in inventory.
    /// </summary>
    /// <returns></returns>
    Vector2Int FindItemPlacement(GameObject itemObj)
    {
        Debug.Log("Inventory::FindItemPlacement");
        InventoryItem item = itemObj.GetComponent<InventoryItem>();
        int maxxCheck = xSquares - item.width + 1;
        int maxyCheck = ySquares - item.height + 1;
        for (int y = 0; y < maxyCheck; ++y)
        {
            for (int x = 0; x < maxxCheck; ++x)
            {
                bool collision = CheckCollisions(itemObj, x, y, item.width, item.height);
                if (!collision)
                {
                    Vector2Int freeSpot = new Vector2Int(x, y);
                    return freeSpot;
                }
            }
        }

        Vector2Int noFreeSpot = new Vector2Int(-1, -1);
        return noFreeSpot;
    }

    /// <summary>
    /// Returns item reference if it can be added to inventory.
    /// </summary>
    /// <returns>Desired item numerical ID in JSON.</returns>
    public InventoryItem AddItem(int id_i)
    {
        Debug.Log("Inventory::AddItem");
        GameObject itemObj = Instantiate(itemPrefab, transform);
        InventoryItem item = itemObj.GetComponent<InventoryItem>();
        item.InitializeItem(id_i);

        Vector2Int spot = FindItemPlacement(itemObj);
        if (spot != new Vector2Int(-1, -1))
        {
            items.Add(itemObj);
            item.positionx = spot.x;
            item.positiony = spot.y;
            return item;
        }

        Destroy(itemObj);
        return null;
    }

    public void AutoSort()
    {
        Dictionary<InventoryItem, Vector2Int> itemInitialPositions = new Dictionary<InventoryItem, Vector2Int>();

        int currentPriority = 0;
        foreach (GameObject item in items)
        {
            InventoryItem inventoryItem = item.GetComponent<InventoryItem>();

            // add initial position to dictionary in case auto-sort doesn't work
            itemInitialPositions.Add(
                inventoryItem, 
                new Vector2Int(
                    inventoryItem.positionx, 
                    inventoryItem.positiony));

            inventoryItem.positionx = -9999;
            inventoryItem.positiony = -9999;
        }

        List<GameObject> unsortedItems = new List<GameObject>(items);

        bool failed = false;

        while (unsortedItems.Count > 0)
        {
            List<GameObject> itemsToRemove = new List<GameObject>();
            foreach (GameObject item in unsortedItems)
            {
                InventoryItem inventoryItem = item.GetComponent<InventoryItem>();
                if (inventoryItem.sortPriority == currentPriority)
                {
                    Vector2Int desiredPos = FindItemPlacement(item);
                    if (desiredPos == new Vector2Int(-1, -1))
                    {
                        failed = true;
                        goto FAILED;
                    }
                    inventoryItem.Move(desiredPos);
                    itemsToRemove.Add(item);
                }
            }
            foreach (GameObject item in itemsToRemove)
            {
                unsortedItems.Remove(item);
            }
            ++currentPriority;
        }

        FAILED:
        if (failed)
        {
            // resetting positions
            foreach (KeyValuePair<InventoryItem, Vector2Int> kvp in itemInitialPositions)
            {
                kvp.Key.Move(kvp.Value);
            }
        }
    }
}
