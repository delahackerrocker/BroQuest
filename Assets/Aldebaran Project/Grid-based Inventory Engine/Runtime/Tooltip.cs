using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Tooltip : MonoBehaviour
{
    [SerializeField, Tooltip("Tooltip text GameObject.")]
    public TextMeshProUGUI text;
    [SerializeField, Tooltip("Tooltip background image.")]
    Image image;

    public float animationDuration = 1.0f;
    public Color offColor;
    public Color onColor;
    public Color offColorText;
    public Color onColorText;

    Color targetColor;
    Color currentColor;
    Color targetColorText;
    Color currentColorText;


    float timer = 0.0f;

    Item currentItem, previousItem;

    void Update()
    {
        currentItem = InventoryCommon.highlitedItem;


        if (InventoryCommon.highlitedItem != null)
        {
            text.SetText(InventoryCommon.highlitedItem.name);
        }
        // else
        // {
        //     text.SetText("");
        // }
        transform.position = Input.mousePosition;    


        if (previousItem != currentItem)
        {
            timer = 0.0f;
            currentColor = image.color;
            currentColorText = text.color;

            if (currentItem != null)
            {
                targetColor = onColor;
                targetColorText = onColorText;
            }
            else
            {
                targetColor = offColor;
                targetColorText = offColorText;
            }

        }
        if (timer < 0.998f)
        {
            timer += Time.deltaTime / animationDuration;
            image.color = Color.Lerp(currentColor, targetColor, timer);
            text.color = Color.Lerp(currentColorText, targetColorText, timer * 6.0f);
        }


        previousItem = currentItem;
    }
}
