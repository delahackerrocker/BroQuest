using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
public class InventoryItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public int id_i;
    [HideInInspector] public int width = 1; 
    [HideInInspector] public int height = 1;
    public int positionx;
    public int positiony;

    public GameObject itemHighlightPrefab;
    public GameObject itemHighlight;
    public GameObject representedItem;

    RectTransform rect;
    RectTransform highlightRect;
    Inventory inventory;
    Canvas canvas;
    Image image;

    Item m_item;
    public Item M_item 
    {
        get { return m_item; }
    }


    [Header("Debug")]
    [SerializeField]
    public int sortPriority;
    [SerializeField]
    bool grabbed;
    [SerializeField]
    bool collision;
    [SerializeField]
    bool onSide;
    [SerializeField]
    bool onSideWhenGrabbed;

    /// <summary>
    /// Generates item according to id.
    /// </summary>
    /// <param name="id_i">Numerical item id in JSON file.</param>
    public void InitializeItem(int id_i)
    {
        this.id_i = id_i;

        UpdateProperties();
    }

    void UpdateProperties()
    {
        bool foundItem = false;

        foreach (Item item in ItemList.items)
        {
            if (item.id_i == id_i)
            {
                m_item = item;
                foundItem = true;
                break;
            }
        }

        if (!foundItem)
        {
            m_item = ItemList.items[0];
            id_i = ItemList.items[0].id_i;
        }

        width = m_item.width;
        height = m_item.height;
        ItemList.types.TryGetValue(m_item.type, out sortPriority);

        Texture2D texture = Resources.Load<Texture2D>(m_item.spritePath);
        Sprite sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);
        image.sprite = sprite;

        representedItem = Resources.Load<GameObject>(m_item.prefabPath);

        gameObject.name = m_item.name;
    }

    private void OnValidate()
    {
        UpdateGridPositionAndSize();
    }

    private void Awake()
    {
        canvas = transform.parent.parent.parent.GetComponent<Canvas>();
        itemHighlight = Instantiate(itemHighlightPrefab, transform.position, Quaternion.identity);
        itemHighlight.transform.SetParent(transform.parent, false);

        highlightRect = itemHighlight.GetComponent<RectTransform>();
        highlightRect.pivot = new Vector2(0.5f, 0.5f);
        highlightRect.anchorMin = new Vector2(0.0f, 1.0f);
        highlightRect.anchorMax = new Vector2(0.0f, 1.0f);

        image = GetComponent<Image>();
    }

    private void Start()
    {
        UpdateProperties();
        
        rect = GetComponent<RectTransform>();
        inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();

        foreach (GameObject item in inventory.representableItems)
        {
            if (item.CompareTag(m_item.type))
            {
                representedItem = item;
                break;
            }
        }
        
        UpdateGridPositionAndSize();

        float aspect = 1.0f;
        int xResolution = 256, yResolution = 256;
        if (height > width)
        {
            aspect = height / width;
            yResolution = (int)(yResolution * aspect);
        }
        else if (width > height)
        {
            aspect = width / height;
            xResolution = (int)(xResolution * aspect);
        }
        //Sprite thumbSprite = Sprite.Create(thumbnail, new Rect(0.0f, 0.0f, thumbnail.width, thumbnail.height), new Vector2(0.5f, 0.5f));
        //GetComponent<Image>().sprite = thumbSprite;

        UpdateHighlight();
        itemHighlight.SetActive(false);
    }

    BaseEventData m_BaseEvent;
    private void Update()
    {
        FollowCursor();
    }

    public void Grab()
    {
        if (grabbed) // drop item
        {
            int checkPositionx = (int)((rect.anchoredPosition.x - ((onSide ? rect.sizeDelta.y : rect.sizeDelta.x) / 2.0f) + inventory.squareSize / 2.0f  + (inventory.padding / 2.0f)) / inventory.squareSize) - 1;
            int checkPositiony = (int)((-rect.anchoredPosition.y - ((onSide ? rect.sizeDelta.x : rect.sizeDelta.y) / 2.0f)  + inventory.squareSize / 2.0f +  (inventory.padding / 2.0f)) / inventory.squareSize) - 1;
            bool failed = true;
            if (inventory.CheckInsideBounds(checkPositionx, checkPositiony, width, height) && !collision)
            {
                positionx = checkPositionx;
                positiony = checkPositiony;
                failed = false;
            }

            Drop(failed);
        }
        else if (!inventory.GrabbingItem()) // grab item
        {
            highlightRect.sizeDelta = new Vector2(width * inventory.squareSize, height * inventory.squareSize);
            grabbed = true;
            onSideWhenGrabbed = onSide;
            //Cursor.visible = false;
            Cursor.visible = true;
            inventory.GrabbedItem = this;

            // reorder images - icon on top, highlight second, all above other items and squares
            Transform temp = transform.parent;
            itemHighlight.transform.SetParent(null, false);
            itemHighlight.transform.SetParent(temp, false);
            transform.SetParent(null, false);
            transform.SetParent(temp, false);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="failed">Should be true if failed to place object somewhere else.</param>
    public void Drop(bool failed)
    {
        inventory.GrabbedItem = null;
        grabbed = false;
        Cursor.visible = true;
        itemHighlight.SetActive(false);
        if (failed)
        {
            if (onSideWhenGrabbed && !onSide)
            {
                Rotate();
            }
            if (!onSideWhenGrabbed && onSide)
            {
                Rotate();
            }
        }
        UpdateGridPositionAndSize();
    }

    public void Select()
    {
        inventory.SelectItem(m_item);
    }

    void UpdateHighlight()
    {
        highlightRect.sizeDelta = new Vector2(width * inventory.squareSize, height * inventory.squareSize);
        highlightRect.anchoredPosition = new Vector2(
            positionx * inventory.squareSize + (inventory.padding / 2.0f) + (highlightRect.sizeDelta.x / 2f),
            -positiony * inventory.squareSize - (inventory.padding / 2.0f) - (highlightRect.sizeDelta.y / 2f)
            );
        highlightRect.GetComponent<Image>().color = highlightRect.GetComponent<InventoryHighlight>().highlightColor;
    }

    void UpdateHighlightPosition()
    {
        highlightRect.anchoredPosition = new Vector2(
            positionx * inventory.squareSize + (inventory.padding / 2.0f) + (highlightRect.sizeDelta.x / 2f),
            -positiony * inventory.squareSize - (inventory.padding / 2.0f) - (highlightRect.sizeDelta.y / 2f)
            );
        highlightRect.GetComponent<Image>().color = highlightRect.GetComponent<InventoryHighlight>().highlightColor;
    }

    void FollowCursor()
    {
        if (!grabbed) 
            return;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            Input.mousePosition, canvas.worldCamera,
            out Vector2 movePos);

        // Offset for how the selected item hovers under the mouse
        Vector3 offset = new Vector3(-rect.sizeDelta.x * canvas.scaleFactor, rect.sizeDelta.y * canvas.scaleFactor, 0.0f) / 2.0f;
        transform.position = canvas.transform.TransformPoint(movePos) + offset;
        rect.anchoredPosition += new Vector2(rect.sizeDelta.x / 2.0f, -rect.sizeDelta.y / 2.0f);

        int checkPositionx = (int)((rect.anchoredPosition.x - ((onSide ? rect.sizeDelta.y : rect.sizeDelta.x) / 2.0f)  + inventory.squareSize / 2.0f + (inventory.padding / 2.0f)) / inventory.squareSize) - 1;
        int checkPositiony = (int)((-rect.anchoredPosition.y - ((onSide ? rect.sizeDelta.x : rect.sizeDelta.y) / 2.0f) + inventory.squareSize / 2.0f + (inventory.padding / 2.0f)) / inventory.squareSize) - 1;
        if (inventory.CheckInsideBounds(checkPositionx, checkPositiony, width, height))
        {
            itemHighlight.SetActive(true);
            highlightRect.anchoredPosition = new Vector2(
                checkPositionx * inventory.squareSize + (inventory.padding / 2.0f) + (highlightRect.sizeDelta.x / 2f),
                -checkPositiony * inventory.squareSize - (inventory.padding / 2.0f) - (highlightRect.sizeDelta.y / 2f)
                );
            //highlightRect.anchoredPosition += new Vector2(highlightRect.sizeDelta.x / 2.0f, -highlightRect.sizeDelta.y / 2.0f);

            // check for collisions
            collision = inventory.CheckCollisions(gameObject, checkPositionx, checkPositiony, onSide ? width : width, onSide ? height : height);
            
            if (collision)
            {
                highlightRect.GetComponent<Image>().color = highlightRect.GetComponent<InventoryHighlight>().collisionColor;
            }
            else
            {
                highlightRect.GetComponent<Image>().color = highlightRect.GetComponent<InventoryHighlight>().highlightColor;
            }
        }
        else
        {
            itemHighlight.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Rotate();
        } else if (Input.GetKeyDown(KeyCode.Backspace))
        {
            Remove();
        }
    }

    public void Remove()
    {
        Destroy(itemHighlight);
        inventory.RemoveItem(this);
    }

    public void Rotate()
    {
        Vector2 tempPosition = rect.anchoredPosition;
        if (!onSide)
        {
            rect.localEulerAngles = new Vector3(0.0f, 0.0f, -90.0f);
            onSide = true;
        }
        else
        {
            rect.localEulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
            onSide = false;
        }    
        int temp = width;
        width = height;
        height = temp;

        UpdateGridSize();
    }

    public void UpdateGridPositionAndSize()
    {
        if (rect == null)
            return;

        highlightRect.sizeDelta = new Vector2(width * inventory.squareSize, height * inventory.squareSize);
        rect.sizeDelta = new Vector2((onSide ? height : width) * inventory.squareSize, (onSide ? width :height) * inventory.squareSize);
        //rect.localScale = new Vector3((onSide ? height : width) * inventory.squareSize, (onSide ? width :height) * inventory.squareSize, 1.0f);
        rect.anchoredPosition = new Vector2(
            positionx * inventory.squareSize + (inventory.padding / 2.0f) + (onSide ? rect.sizeDelta.y : rect.sizeDelta.x) / 2.0f,
            -positiony * inventory.squareSize - (inventory.padding / 2.0f) - (onSide ? rect.sizeDelta.x : rect.sizeDelta.y) / 2.0f
            );
    }

    void UpdateGridPosition()
    {
        if (rect == null)
            return;

        rect.anchoredPosition = new Vector2(
            positionx * inventory.squareSize + (inventory.padding / 2.0f) + (onSide ? rect.sizeDelta.y : rect.sizeDelta.x) / 2.0f,
            -positiony * inventory.squareSize - (inventory.padding / 2.0f) - (onSide ? rect.sizeDelta.x : rect.sizeDelta.y) / 2.0f
            );
    }

    public void UpdateGridSize()
    {
        if (rect == null)
            return;

        highlightRect.sizeDelta = new Vector2(width * inventory.squareSize, height * inventory.squareSize);
    }
    
    public void Move(Vector2Int newPosition)
    {
        positionx = newPosition.x;
        positiony = newPosition.y;

        UpdateGridPosition();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        UpdateHighlightPosition();
        itemHighlight.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        itemHighlight.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (!inventory.GrabbingItem())
            {
                Select();
            }
            Grab();
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (!grabbed)
                InventoryCommon.contextMenu.Show(this);
        }
    }
}
