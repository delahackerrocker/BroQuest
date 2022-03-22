using UnityEngine;

public class CharacterScreen : MonoBehaviour
{
    [SerializeField]
    UIBackground uiBackground;

    bool isOpen = false;

    private void Start()
    {
        isOpen = gameObject.activeSelf;
    }

    /// <summary>
    /// Closes character screen.
    /// </summary>
    public void Close()
    {
        InventoryCommon.SetControls(false, true);
        isOpen = false;
        uiBackground.TurnOff();

        gameObject.SetActive(false);
    }

    /// <summary>
    /// Opens character screen.
    /// </summary>
    public void Open()
    {
        gameObject.SetActive(true);

        InventoryCommon.SetControls(true, false);
        isOpen = true;
        uiBackground.TurnOn();
    }

    /// <summary>
    /// Toggles character screen open or closed.
    /// </summary>
    public void ToggleOpen()
    {
        if (isOpen)
        {
            Close();
        }
        else
        {
            Open();
        }
    }
}
