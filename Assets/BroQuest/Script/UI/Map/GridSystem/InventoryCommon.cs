using UnityEngine;

class InventoryCommon
{
    public static ContextMenu contextMenu;

    /// <summary>
    /// Whether the player can move.
    /// </summary>
    public static bool lockedControls = false;

    /// <summary>
    /// Item currently highlited by cursor. Used by the Tooltip class.
    /// </summary>
    public static Item highlitedItem = null;

    /// <summary>
    /// Sets player controls and cursor state.
    /// </summary>
    /// <param name="controlsLocked">Should the player be able to move?</param>
    /// <param name="cursorLocked">Should the cursor be hidden and locked?</param>
    public static void SetControls(bool controlsLocked, bool cursorLocked)
    {
        lockedControls = controlsLocked;
        Cursor.lockState = cursorLocked ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !cursorLocked;
    }
}
