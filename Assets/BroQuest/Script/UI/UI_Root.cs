using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Root : MonoBehaviour
{
    public CanvasController splashScreen;
    public CanvasController startScreen;
    public CanvasController actionBars;
    public CanvasController chatWindow;
    public CanvasController diceArena;
    //public CanvasController rollLogWindow;
    //public CanvasController treasureDeckWindow;
    //public CanvasController playerActionSideBar;
    //public CanvasController zargonActionSideBar;

    public static UI_Root instance;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        splashScreen.gameObject.SetActive(true);
    }

    void Update()
    {
        
    }

    public void GameUI()
    {
        HideAll();
        actionBars.TurnOnCanvas();
    }

    public void DiceUI()
    {
        HideAll();
        actionBars.TurnOnCanvas();
        diceArena.TurnOnCanvas();
    }

    public void ChatUI()
    {
        HideAll();
        actionBars.TurnOnCanvas();

        if (chatWindow.isActiveAndEnabled)
        {
            chatWindow.TurnOffCanvas();
        } else
        {
            chatWindow.TurnOnCanvas();
        }
    }

    private void HideAll()
    {
        splashScreen.TurnOffCanvas();
        startScreen.TurnOffCanvas();
        actionBars.TurnOffCanvas();
        chatWindow.TurnOffCanvas();
        diceArena.TurnOffCanvas();
        //rollLogWindow.TurnOffCanvas();
        //treasureDeckWindow.TurnOffCanvas();
        //playerActionSideBar.TurnOffCanvas();
        //zargonActionSideBar.TurnOffCanvas();
    }
}
